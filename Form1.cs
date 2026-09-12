using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClipEditor
{
    public partial class Form1 : Form
    {
        private const int WmNcLeftButtonDown = 0x00A1;
        private const int HtCaption = 2;
        private const int ClipboardRetryCount = 5;
        private const int ClipboardRetryDelayMilliseconds = 60;
        private const int MaxClipboardHistoryEntries = 20;
        private const int ClipboardHistoryPreviewLength = 18;
        private const int ClipboardHistoryCloseSize = 18;
        private const int PreferredMinimumWindowWidth = 1050;
        private const int PreferredMinimumWindowHeight = 660;
        private const int WmClipboardUpdate = 0x031D;
        private const string DeveloperStoreUrl =
            "https://store.steampowered.com/search?term=Alright+Peaches+Studio";
        private const string RegistryPath =
            @"Software\Alright Peaches Studio\ClipEditor";
        private const string RegistryLanguageValueName = "UiLanguage";
        private const string RegistryModeValueName = "ProcessingMode";
        private const string RegistryClipboardWatchValueName =
            "ClipboardWatchMode";

        private bool useEnglish;
        private bool initializingPreferences;
        private bool clipboardListenerRegistered;
        private bool changingHistorySelection;
        private bool normalizingEditorLineEndings;
        private string lastClipboardHistoryText;
        private int nextClipboardHistoryNumber;
        private TabPage hoveredHistoryCloseTab;
        private TabPage pressedHistoryCloseTab;

        private sealed class ClipboardHistoryEntry
        {
            public ClipboardHistoryEntry(int number, string text)
            {
                Number = number;
                Text = text ?? string.Empty;
                CreatedAt = DateTime.Now;
            }

            public int Number { get; private set; }

            public string Text { get; set; }

            public DateTime CreatedAt { get; private set; }
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AddClipboardFormatListener(IntPtr windowHandle);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RemoveClipboardFormatListener(IntPtr windowHandle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(
            IntPtr windowHandle,
            int message,
            IntPtr wParam,
            IntPtr lParam);

        private const string ChineseListNumberPattern =
            "二十四|二十三|二十二|二十一|二十|十九|十八|十七|" +
            "十六|十五|十四|十三|十二|十一|十|九|八|七|六|五|四|三|二|一";

        private static readonly Regex CommonListPrefixRegex = new Regex(
            @"^[ \t]*(?:(?:" +
            @"[（(]\d{1,3}[）)][、.]?|" +
            @"<\d{1,3}>、?|" +
            @"\d{1,3}(?:\)\.|）\.|\)、|）、|[、）)>]|\.(?!\d))|" +
            @"(?:" + ChineseListNumberPattern + @")、|" +
            @"[（(](?:" + ChineseListNumberPattern + @")[）)]、?" +
            @")[ \t]*)+",
            RegexOptions.Compiled | RegexOptions.Multiline);

        public Form1()
        {
            InitializeComponent();
            RegisterWindowDragHandlers(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializingPreferences = true;
            try
            {
                LoadLanguagePreference();
                LoadModePreference();
                LoadClipboardWatchPreference();
            }
            finally
            {
                initializingPreferences = false;
            }

            ApplyLanguage();
            InitializeClipboardHistory();
            EnsureClipboardListenerAvailable();
            TryLoadClipboard(
                showSuccessMessage: false,
                clearEditorWhenNoText: liveClipboardRadioButton.Checked);
            EnsureClipboardHistoryEntry();
            UpdateTextStatistics();
            ApplyScreenSizeLimits();
        }

        private void ApplyScreenSizeLimits()
        {
            // 先完成一次布局，才能准确算出标题栏、工具栏等非编辑区所占的高度。
            this.PerformLayout();
            rootLayout.PerformLayout();

            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            int maximumWindowWidth = Math.Max(1, workingArea.Width);
            int maximumWindowHeight = Math.Max(1, workingArea.Height);
            int minimumWindowWidth = Math.Min(
                PreferredMinimumWindowWidth,
                maximumWindowWidth);
            int minimumWindowHeight = Math.Min(
                PreferredMinimumWindowHeight,
                maximumWindowHeight);

            // 文本框不单独限制高度，由 rootLayout 的百分比行占满
            // Processing Mode 上方的全部剩余空间；窗体本身负责屏幕限制。
            this.MinimumSize = new Size(
                minimumWindowWidth,
                minimumWindowHeight);
            this.MaximumSize = new Size(
                maximumWindowWidth,
                maximumWindowHeight);
            this.MaximizedBounds = workingArea;

            if (this.WindowState == FormWindowState.Normal)
            {
                if (this.Width > maximumWindowWidth)
                {
                    this.Width = maximumWindowWidth;
                }

                if (this.Height > maximumWindowHeight)
                {
                    this.Height = maximumWindowHeight;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TryLoadClipboard(showSuccessMessage: true);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (normalizingEditorLineEndings)
            {
                return;
            }

            NormalizeEditorLineEndingsIfNeeded();
            UpdateTextStatistics();
            UpdateSelectedHistoryEntry();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TryCopyToClipboard(
                textBox1.Text,
                Localize(
                    "文本已复制，可去别的地方直接粘贴了",
                    "Text copied to the clipboard"));
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void HistoryTabControl_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (changingHistorySelection ||
                historyTabControl.SelectedTab == null)
            {
                return;
            }

            ClipboardHistoryEntry entry =
                historyTabControl.SelectedTab.Tag as ClipboardHistoryEntry;
            if (entry == null)
            {
                return;
            }

            changingHistorySelection = true;
            try
            {
                SetEditorText(
                    entry.Text,
                    preserveSelection: false,
                    synchronizeClipboard: false);
            }
            finally
            {
                changingHistorySelection = false;
            }

            textBox1.Focus();
        }

        private void HistoryTabControl_Selecting(
            object sender,
            TabControlCancelEventArgs e)
        {
            if (changingHistorySelection ||
                Control.MouseButtons != MouseButtons.Left ||
                e.TabPageIndex < 0)
            {
                return;
            }

            Point mousePosition = historyTabControl.PointToClient(
                Cursor.Position);
            Rectangle closeBounds = GetHistoryTabCloseRectangle(
                historyTabControl.GetTabRect(e.TabPageIndex));

            // 点击关闭区时不切换标签，关闭和切换始终使用两个独立区域。
            if (!closeBounds.IsEmpty && closeBounds.Contains(mousePosition))
            {
                e.Cancel = true;
            }
        }

        private void HistoryTabControl_DrawItem(
            object sender,
            DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= historyTabControl.TabPages.Count)
            {
                return;
            }

            TabPage page = historyTabControl.TabPages[e.Index];
            Rectangle tabBounds = e.Bounds;
            bool selected =
                (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backgroundColor = selected
                ? SystemColors.Window
                : SystemColors.Control;

            using (SolidBrush backgroundBrush =
                new SolidBrush(backgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, tabBounds);
            }

            using (Pen borderPen = new Pen(
                selected
                    ? SystemColors.Highlight
                    : SystemColors.ControlDark))
            {
                e.Graphics.DrawRectangle(
                    borderPen,
                    tabBounds.X,
                    tabBounds.Y,
                    Math.Max(0, tabBounds.Width - 1),
                    Math.Max(0, tabBounds.Height - 1));
            }

            Rectangle closeBounds = GetHistoryTabCloseRectangle(tabBounds);
            int textRight = closeBounds.IsEmpty
                ? tabBounds.Right - 10
                : closeBounds.Left - 8;
            Rectangle textBounds = new Rectangle(
                tabBounds.Left + 10,
                tabBounds.Top + 1,
                Math.Max(0, textRight - tabBounds.Left - 10),
                Math.Max(0, tabBounds.Height - 2));

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                historyTabControl.Font,
                textBounds,
                SystemColors.ControlText,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.NoPrefix);

            if (!closeBounds.IsEmpty)
            {
                DrawHistoryTabCloseButton(e.Graphics, page, closeBounds);
            }
        }

        private void HistoryTabControl_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            TabPage page = GetHistoryTabFromClosePoint(e.Location);
            if (!ReferenceEquals(pressedHistoryCloseTab, page))
            {
                InvalidateHistoryTab(pressedHistoryCloseTab);
                pressedHistoryCloseTab = page;
                InvalidateHistoryTab(pressedHistoryCloseTab);
            }
        }

        private void HistoryTabControl_MouseUp(
            object sender,
            MouseEventArgs e)
        {
            TabPage pressedPage = pressedHistoryCloseTab;
            pressedHistoryCloseTab = null;
            InvalidateHistoryTab(pressedPage);

            if (e.Button != MouseButtons.Left || pressedPage == null)
            {
                return;
            }

            // 必须在同一个标签的关闭区内按下并松开，才真正删除。
            if (ReferenceEquals(
                pressedPage,
                GetHistoryTabFromClosePoint(e.Location)))
            {
                DeleteHistoryTab(pressedPage);
            }
        }

        private void HistoryTabControl_MouseMove(
            object sender,
            MouseEventArgs e)
        {
            TabPage page = GetHistoryTabFromClosePoint(e.Location);
            if (ReferenceEquals(hoveredHistoryCloseTab, page))
            {
                return;
            }

            TabPage previousPage = hoveredHistoryCloseTab;
            hoveredHistoryCloseTab = page;
            InvalidateHistoryTab(previousPage);
            InvalidateHistoryTab(hoveredHistoryCloseTab);
        }

        private void HistoryTabControl_MouseLeave(
            object sender,
            EventArgs e)
        {
            TabPage previousPage = hoveredHistoryCloseTab;
            TabPage previouslyPressedPage = pressedHistoryCloseTab;
            hoveredHistoryCloseTab = null;
            pressedHistoryCloseTab = null;
            InvalidateHistoryTab(previousPage);
            if (!ReferenceEquals(previousPage, previouslyPressedPage))
            {
                InvalidateHistoryTab(previouslyPressedPage);
            }
        }

        private void InitializeClipboardHistory()
        {
            changingHistorySelection = true;
            try
            {
                historyTabControl.TabPages.Clear();
                nextClipboardHistoryNumber = 0;
                lastClipboardHistoryText = null;
                hoveredHistoryCloseTab = null;
                pressedHistoryCloseTab = null;
            }
            finally
            {
                changingHistorySelection = false;
            }
        }

        private void EnsureClipboardHistoryEntry()
        {
            if (historyTabControl.TabPages.Count == 0)
            {
                AddClipboardHistoryEntry(textBox1.Text);
            }
        }

        private bool LoadClipboardTextIntoHistory(
            string clipboardText,
            bool forceDisplay)
        {
            string newText = NormalizeLineEndings(clipboardText);

            if (string.Equals(
                lastClipboardHistoryText,
                newText,
                StringComparison.Ordinal))
            {
                if (forceDisplay &&
                    !string.Equals(textBox1.Text, newText, StringComparison.Ordinal))
                {
                    if (!SelectMostRecentHistoryEntry(newText))
                    {
                        AddClipboardHistoryEntry(newText);
                    }

                    return true;
                }

                return false;
            }

            lastClipboardHistoryText = newText;

            // 程序自身刚把相同内容写入剪贴板时，不重复创建标签。
            if (historyTabControl.TabPages.Count > 0 &&
                string.Equals(textBox1.Text, newText, StringComparison.Ordinal))
            {
                return false;
            }

            AddClipboardHistoryEntry(newText);
            return true;
        }

        private bool SelectMostRecentHistoryEntry(string text)
        {
            for (int index = historyTabControl.TabPages.Count - 1;
                index >= 0;
                index--)
            {
                TabPage page = historyTabControl.TabPages[index];
                ClipboardHistoryEntry entry =
                    page.Tag as ClipboardHistoryEntry;
                if (entry == null ||
                    !string.Equals(entry.Text, text, StringComparison.Ordinal))
                {
                    continue;
                }

                changingHistorySelection = true;
                try
                {
                    historyTabControl.SelectedTab = page;
                    SetEditorText(
                        entry.Text,
                        preserveSelection: false,
                        synchronizeClipboard: false);
                }
                finally
                {
                    changingHistorySelection = false;
                }

                return true;
            }

            return false;
        }

        private void AddClipboardHistoryEntry(string text)
        {
            ClipboardHistoryEntry entry = new ClipboardHistoryEntry(
                ++nextClipboardHistoryNumber,
                text);
            TabPage page = new TabPage();
            page.Name = "clipboardHistoryTab" + entry.Number;
            page.Tag = entry;
            UpdateHistoryTabPresentation(page, entry);

            changingHistorySelection = true;
            try
            {
                historyTabControl.TabPages.Add(page);
                historyTabControl.SelectedTab = page;
                SetEditorText(
                    entry.Text,
                    preserveSelection: false,
                    synchronizeClipboard: false);

                while (historyTabControl.TabPages.Count >
                    MaxClipboardHistoryEntries)
                {
                    TabPage oldestPage = historyTabControl.TabPages[0];
                    if (ReferenceEquals(hoveredHistoryCloseTab, oldestPage))
                    {
                        hoveredHistoryCloseTab = null;
                    }

                    if (ReferenceEquals(pressedHistoryCloseTab, oldestPage))
                    {
                        pressedHistoryCloseTab = null;
                    }

                    historyTabControl.TabPages.RemoveAt(0);
                    oldestPage.Dispose();
                }
            }
            finally
            {
                changingHistorySelection = false;
            }
        }

        private void UpdateSelectedHistoryEntry()
        {
            if (changingHistorySelection ||
                historyTabControl == null ||
                historyTabControl.SelectedTab == null)
            {
                return;
            }

            ClipboardHistoryEntry entry =
                historyTabControl.SelectedTab.Tag as ClipboardHistoryEntry;
            if (entry == null)
            {
                return;
            }

            entry.Text = textBox1.Text;
            UpdateHistoryTabPresentation(
                historyTabControl.SelectedTab,
                entry);
        }

        private void RefreshHistoryTabTitles()
        {
            if (historyTabControl == null)
            {
                return;
            }

            foreach (TabPage page in historyTabControl.TabPages)
            {
                ClipboardHistoryEntry entry =
                    page.Tag as ClipboardHistoryEntry;
                if (entry != null)
                {
                    UpdateHistoryTabPresentation(page, entry);
                }
            }
        }

        private void UpdateHistoryTabPresentation(
            TabPage page,
            ClipboardHistoryEntry entry)
        {
            string preview = Regex.Replace(
                entry.Text ?? string.Empty,
                @"\s+",
                " ").Trim();
            if (preview.Length == 0)
            {
                preview = Localize("（空）", "(empty)");
            }
            else if (preview.Length > ClipboardHistoryPreviewLength)
            {
                preview = preview.Substring(0, ClipboardHistoryPreviewLength) + "…";
            }

            page.Text = entry.Number.ToString() + " · " +
                entry.CreatedAt.ToString("HH:mm:ss") + " · " + preview;
            page.ToolTipText = Localize("字符数：", "Characters: ") +
                entry.Text.Length.ToString("N0") +
                Localize("；点击右侧 × 删除", "; click the × on the right to delete");
            InvalidateHistoryTab(page);
        }

        private Rectangle GetHistoryTabCloseRectangle(Rectangle tabBounds)
        {
            if (tabBounds.Width < ClipboardHistoryCloseSize + 40 ||
                tabBounds.Height <= 0)
            {
                return Rectangle.Empty;
            }

            int closeSize = Math.Min(
                ClipboardHistoryCloseSize,
                Math.Max(0, tabBounds.Height - 8));
            if (closeSize <= 0)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(
                tabBounds.Right - closeSize - 8,
                tabBounds.Top + (tabBounds.Height - closeSize) / 2,
                closeSize,
                closeSize);
        }

        private TabPage GetHistoryTabFromClosePoint(Point location)
        {
            for (int index = 0;
                index < historyTabControl.TabPages.Count;
                index++)
            {
                Rectangle closeBounds = GetHistoryTabCloseRectangle(
                    historyTabControl.GetTabRect(index));
                if (!closeBounds.IsEmpty && closeBounds.Contains(location))
                {
                    return historyTabControl.TabPages[index];
                }
            }

            return null;
        }

        private void DrawHistoryTabCloseButton(
            Graphics graphics,
            TabPage page,
            Rectangle closeBounds)
        {
            bool pressed = ReferenceEquals(pressedHistoryCloseTab, page);
            bool hovered = ReferenceEquals(hoveredHistoryCloseTab, page);

            if (pressed || hovered)
            {
                Color closeBackground = pressed
                    ? Color.FromArgb(190, 45, 45)
                    : Color.FromArgb(222, 70, 65);
                using (SolidBrush closeBrush =
                    new SolidBrush(closeBackground))
                {
                    graphics.FillRectangle(closeBrush, closeBounds);
                }
            }

            Color glyphColor = pressed || hovered
                ? Color.White
                : SystemColors.ControlText;
            using (Pen glyphPen = new Pen(glyphColor, 1.8F))
            {
                int inset = 5;
                graphics.DrawLine(
                    glyphPen,
                    closeBounds.Left + inset,
                    closeBounds.Top + inset,
                    closeBounds.Right - inset - 1,
                    closeBounds.Bottom - inset - 1);
                graphics.DrawLine(
                    glyphPen,
                    closeBounds.Right - inset - 1,
                    closeBounds.Top + inset,
                    closeBounds.Left + inset,
                    closeBounds.Bottom - inset - 1);
            }
        }

        private void InvalidateHistoryTab(TabPage page)
        {
            if (page == null || historyTabControl == null)
            {
                return;
            }

            int index = historyTabControl.TabPages.IndexOf(page);
            if (index >= 0)
            {
                historyTabControl.Invalidate(
                    historyTabControl.GetTabRect(index));
            }
        }

        private void DeleteHistoryTab(TabPage page)
        {
            int removedIndex = historyTabControl.TabPages.IndexOf(page);
            if (removedIndex < 0)
            {
                return;
            }

            bool deletingSelectedTab = ReferenceEquals(
                historyTabControl.SelectedTab,
                page);
            changingHistorySelection = true;
            try
            {
                if (ReferenceEquals(hoveredHistoryCloseTab, page))
                {
                    hoveredHistoryCloseTab = null;
                }

                if (ReferenceEquals(pressedHistoryCloseTab, page))
                {
                    pressedHistoryCloseTab = null;
                }

                historyTabControl.TabPages.Remove(page);
                page.Dispose();

                if (deletingSelectedTab &&
                    historyTabControl.TabPages.Count > 0)
                {
                    int nextIndex = Math.Min(
                        removedIndex,
                        historyTabControl.TabPages.Count - 1);
                    historyTabControl.SelectedIndex = nextIndex;

                    ClipboardHistoryEntry nextEntry =
                        historyTabControl.SelectedTab.Tag as
                            ClipboardHistoryEntry;
                    if (nextEntry != null)
                    {
                        SetEditorText(
                            nextEntry.Text,
                            preserveSelection: false,
                            synchronizeClipboard: false);
                    }
                }
            }
            finally
            {
                changingHistorySelection = false;
            }

            if (historyTabControl.TabPages.Count == 0)
            {
                AddClipboardHistoryEntry(string.Empty);
            }

            textBox1.Focus();
        }

        // 兼容 Visual Studio 设计器偶尔自动生成的 Paint 事件绑定。
        private void rootLayout_Paint(object sender, PaintEventArgs e)
        {
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            try
            {
                clipboardListenerRegistered =
                    AddClipboardFormatListener(this.Handle);
            }
            catch (Exception)
            {
                clipboardListenerRegistered = false;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (clipboardUpdateTimer != null)
            {
                try
                {
                    clipboardUpdateTimer.Stop();
                }
                catch (ObjectDisposedException)
                {
                    // Designer 会在窗体句柄销毁前释放组件。
                }
            }

            if (clipboardListenerRegistered)
            {
                try
                {
                    RemoveClipboardFormatListener(this.Handle);
                }
                catch (Exception)
                {
                    // 窗口即将销毁，移除监听失败时无需阻止程序退出。
                }

                clipboardListenerRegistered = false;
            }

            base.OnHandleDestroyed(e);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // 保留原来的剪贴板监听功能。
            if (m.Msg == WmClipboardUpdate &&
                !this.Disposing &&
                !this.IsDisposed &&
                liveClipboardRadioButton != null &&
                clipboardUpdateTimer != null &&
                liveClipboardRadioButton.Checked &&
                !initializingPreferences)
            {
                clipboardUpdateTimer.Stop();
                clipboardUpdateTimer.Start();
            }
        }

        private void RegisterWindowDragHandlers(Control parent)
        {
            // 只把非交互控件注册为拖动区域，保证编辑、点击和标签切换
            // 始终使用控件本身的标准鼠标行为。
            if (!IsInteractiveControl(parent))
            {
                parent.MouseDown += WindowDrag_MouseDown;
            }

            foreach (Control child in parent.Controls)
            {
                RegisterWindowDragHandlers(child);
            }
        }

        private void WindowDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            Control sourceControl = sender as Control;
            if (IsInteractiveControl(sourceControl))
            {
                return;
            }

            ReleaseCapture();
            SendMessage(
                this.Handle,
                WmNcLeftButtonDown,
                new IntPtr(HtCaption),
                IntPtr.Zero);
        }

        private static bool IsInteractiveControl(Control control)
        {
            return control is TextBoxBase ||
                   control is ButtonBase ||
                   control is TabControl ||
                   control is ScrollBar ||
                   control is ComboBox ||
                   control is ListControl ||
                   control is NumericUpDown;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.Text = GetIdleWindowTitle();
            timer1.Stop();
        }

        private void ClipboardUpdateTimer_Tick(object sender, EventArgs e)
        {
            clipboardUpdateTimer.Stop();
            if (!liveClipboardRadioButton.Checked)
            {
                return;
            }

            TryLoadClipboard(
                showSuccessMessage: false,
                clearEditorWhenNoText: true,
                showErrorMessages: false,
                focusEditor: false);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength == 0)
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "请先在文本框中选择要复制的内容。",
                        "Select the text you want to copy first."),
                    Localize("未选择文本", "No text selected"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }

            TryCopyToClipboard(
                textBox1.SelectedText,
                Localize(
                    "已复制选中的内容，可去别的地方直接粘贴了",
                    "Selected text copied to the clipboard"));
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                ExecuteClipboardOperation(Clipboard.Clear);
                ShowStatus(Localize("剪贴板已清空", "Clipboard cleared"));
                textBox1.Focus();
            }
            catch (ExternalException)
            {
                ShowClipboardUnavailableMessage();
            }
            catch (ThreadStateException)
            {
                ShowStaThreadMessage();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            string oldValue = textBox2.Text;
            if (string.IsNullOrEmpty(oldValue))
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "要被替换的内容不能为空。",
                        "The text to find cannot be empty."),
                    Localize("无法替换", "Cannot replace"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }

            int replacementCount = CountOccurrences(textBox1.Text, oldValue);
            if (replacementCount == 0)
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "要替换的内容在文本框中不存在。",
                        "The text to find does not exist in the editor."),
                    Localize("未找到内容", "Text not found"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }

            if (string.Equals(oldValue, textBox3.Text, StringComparison.Ordinal))
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "替换前后的内容相同，文本未发生变化。",
                        "The find and replacement text are identical."),
                    Localize("文本未变化", "Text unchanged"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }

            SetEditorText(textBox1.Text.Replace(oldValue, textBox3.Text));
            ShowStatus(
                Localize("已替换 ", "Replaced ") + replacementCount +
                Localize(" 处内容", " occurrence(s)"));
            textBox1.Focus();
        }

        private void ButtonFindNext_Click(object sender, EventArgs e)
        {
            FindNextOccurrence();
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            FindNextOccurrence();
        }

        private void FindNextOccurrence()
        {
            string searchText = textBox2.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "请输入要查找的内容。",
                        "Enter the text you want to find."),
                    Localize("无法查找", "Cannot find"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }

            string editorText = textBox1.Text;
            int startIndex = Math.Min(
                textBox1.SelectionStart + textBox1.SelectionLength,
                editorText.Length);
            int matchIndex = editorText.IndexOf(
                searchText,
                startIndex,
                StringComparison.CurrentCultureIgnoreCase);
            bool wrappedToStart = false;

            if (matchIndex < 0 && startIndex > 0)
            {
                matchIndex = editorText.IndexOf(
                    searchText,
                    0,
                    StringComparison.CurrentCultureIgnoreCase);
                wrappedToStart = matchIndex >= 0;
            }

            if (matchIndex < 0)
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "没有找到与查找内容匹配的文本。",
                        "No matching text was found."),
                    Localize("查找完成", "Find complete"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }

            textBox1.Focus();
            textBox1.Select(matchIndex, searchText.Length);
            textBox1.ScrollToCaret();
            ShowStatus(
                wrappedToStart
                    ? Localize(
                        "已从开头继续查找并选中匹配项",
                        "Search wrapped to the beginning; match selected")
                    : Localize("已选中匹配项", "Match selected"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SetEditorText(textBox1.Text.Replace(" ", ""));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SetEditorText(CommonListPrefixRegex.Replace(textBox1.Text, string.Empty));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SetEditorText(textBox1.Text.ToUpper());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SetEditorText(textBox1.Text.ToLower());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SetEditorText(
                textBox1.Text
                    .Replace(" ", "")
                    .Replace("\t", "")
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Trim());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string[] lines = SplitLines(textBox1.Text);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = Regex.Replace(lines[i], @"^[ \t]*\d+[ \t]+", string.Empty)
                    .TrimStart();
            }

            SetEditorText(string.Join(Environment.NewLine, lines));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SetEditorText(textBox1.Text.Replace("->", "").Replace(">>>", ""));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo cultureInfo =
                System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            SetEditorText(textInfo.ToTitleCase(textBox1.Text));
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SetEditorText(string.Empty);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string[] lines = SplitLines(textBox1.Text)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = (i + 1) + ". " + lines[i];
            }

            SetEditorText(string.Join(Environment.NewLine, lines));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string[] lines = SplitLines(textBox1.Text);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].TrimStart();
            }

            SetEditorText(string.Join(Environment.NewLine, lines));
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string[] lines = SplitLines(textBox1.Text);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].TrimStart();
                if (line.StartsWith("-"))
                {
                    line = line.Substring(1).TrimStart();
                }

                lines[i] = line;
            }

            SetEditorText(string.Join(Environment.NewLine, lines));
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string[] lines = SplitLines(textBox1.Text)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();
            SetEditorText(string.Join(Environment.NewLine, lines));
        }

        private void button21_Click(object sender, EventArgs e)
        {
            SetEditorText(RemoveNumberedTitles(textBox1.Text));
        }

        private static string RemoveNumberedTitles(string input)
        {
            string pattern = @"^[ \t]*\d+\.(?!\d)[ \t]*";
            string result = Regex.Replace(input, pattern, string.Empty, RegexOptions.Multiline);
            return result;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            SetEditorText(RemoveLeadingDot(textBox1.Text));
        }

        public static string RemoveLeadingDot(string input)
        {
            // 匹配每行句首前的点号及其左右的空格
            string pattern = @"^[ \t]*\.[ \t]*";

            // 使用正则表达式替换匹配到的内容为空字符串
            string result = Regex.Replace(input, pattern, string.Empty, RegexOptions.Multiline);

            return result;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string[] lines = SplitLines(textBox1.Text);
            List<string> outputLines = new List<string>();
            StringBuilder paragraph = new StringBuilder();

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (paragraph.Length > 0)
                    {
                        paragraph.Append(' ');
                    }

                    paragraph.Append(line.TrimEnd());
                }
                else
                {
                    FlushParagraph(outputLines, paragraph);
                    outputLines.Add(string.Empty);
                }
            }

            FlushParagraph(outputLines, paragraph);
            SetEditorText(string.Join(Environment.NewLine, outputLines));
        }

        private void buttonIndent2_Click(object sender, EventArgs e)
        {
            AddLeadingSpaces(2);
        }

        private void buttonIndent4_Click(object sender, EventArgs e)
        {
            AddLeadingSpaces(4);
        }

        private void buttonIndent8_Click(object sender, EventArgs e)
        {
            AddLeadingSpaces(8);
        }

        private void buttonIndent12_Click(object sender, EventArgs e)
        {
            AddLeadingSpaces(12);
        }

        private void AddLeadingSpaces(int count)
        {
            if (count <= 0)
            {
                return;
            }

            string prefix = new string(' ', count);
            string[] lines = SplitLines(textBox1.Text);
            for (int i = 0; i < lines.Length; i++)
            {
                // 空行保持为空，避免产生肉眼不可见但实际存在的尾随空格。
                if (lines[i].Length > 0)
                {
                    lines[i] = prefix + lines[i];
                }
            }

            SetEditorText(string.Join(Environment.NewLine, lines));
        }

        private void buttonLanguage_Click(object sender, EventArgs e)
        {
            useEnglish = !useEnglish;
            ApplyLanguage();
            ApplyScreenSizeLimits();

            if (!SaveLanguagePreference())
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "界面语言已经切换，但无法保存设置。下次启动时可能恢复为默认语言。",
                        "The interface language changed, but the setting could not be saved."),
                    Localize("设置未保存", "Setting not saved"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void DirectClipboardModeRadioButton_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (initializingPreferences)
            {
                return;
            }

            if (!SaveModePreference())
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "处理模式已经切换，但无法保存设置。下次启动时可能恢复为常规模式。",
                        "The processing mode changed, but the setting could not be saved."),
                    Localize("设置未保存", "Setting not saved"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ShowStatus(
                directClipboardModeRadioButton.Checked
                    ? Localize(
                        "已切换为直接修改剪贴板模式",
                        "Direct clipboard mode selected")
                    : Localize(
                        "已切换为常规模式",
                        "Normal mode selected"));
        }

        private void LiveClipboardRadioButton_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (initializingPreferences)
            {
                return;
            }

            if (liveClipboardRadioButton.Checked &&
                !clipboardListenerRegistered)
            {
                initializingPreferences = true;
                try
                {
                    startupClipboardRadioButton.Checked = true;
                }
                finally
                {
                    initializingPreferences = false;
                }

                clipboardUpdateTimer.Stop();
                SaveClipboardWatchPreference();
                ShowClipboardListenerUnavailableMessage();
                return;
            }

            if (!SaveClipboardWatchPreference())
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "剪贴板监听模式已经切换，但无法保存设置。下次启动时可能恢复为仅启动时导入。",
                        "The clipboard listening mode changed, but the setting could not be saved."),
                    Localize("设置未保存", "Setting not saved"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            if (liveClipboardRadioButton.Checked)
            {
                TryLoadClipboard(
                    showSuccessMessage: false,
                    clearEditorWhenNoText: true,
                    focusEditor: false);
            }
            else
            {
                clipboardUpdateTimer.Stop();
            }

            ShowStatus(
                liveClipboardRadioButton.Checked
                    ? Localize(
                        "已启用剪贴板实时监听",
                        "Live clipboard monitoring enabled")
                    : Localize(
                        "已切换为仅启动时导入剪贴板",
                        "Import clipboard at startup only"));
        }

        private void buttonDeveloperApps_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = DeveloperStoreUrl,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "无法打开默认浏览器。请检查系统的浏览器设置。",
                        "The default browser could not be opened. Check your browser settings.") +
                    Environment.NewLine + ex.Message,
                    Localize("无法打开网页", "Cannot open web page"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private bool TryLoadClipboard(
            bool showSuccessMessage,
            bool clearEditorWhenNoText = false,
            bool showErrorMessages = true,
            bool focusEditor = true)
        {
            try
            {
                string clipboardText = ExecuteClipboardOperation(
                    () => Clipboard.ContainsText()
                        ? Clipboard.GetText(TextDataFormat.UnicodeText)
                        : null);

                if (clipboardText == null)
                {
                    if (clearEditorWhenNoText)
                    {
                        LoadClipboardTextIntoHistory(
                            string.Empty,
                            forceDisplay: false);
                    }

                    if (showSuccessMessage)
                    {
                        MessageBox.Show(
                            this,
                            Localize(
                                "剪贴板中没有可读取的文本，原有内容未被更改。",
                                "The clipboard does not contain readable text. The editor was not changed."),
                            Localize("没有文本", "No text"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    return false;
                }

                LoadClipboardTextIntoHistory(
                    clipboardText,
                    forceDisplay: showSuccessMessage);
                if (showSuccessMessage)
                {
                    ShowStatus(
                        Localize(
                            "已将剪贴板内容载入文本框",
                            "Clipboard text loaded into the editor"));
                }

                if (focusEditor)
                {
                    textBox1.Focus();
                }

                return true;
            }
            catch (ExternalException)
            {
                if (showErrorMessages)
                {
                    ShowClipboardUnavailableMessage();
                }

                return false;
            }
            catch (ThreadStateException)
            {
                if (showErrorMessages)
                {
                    ShowStaThreadMessage();
                }

                return false;
            }
        }

        private bool TryCopyToClipboard(string value, string successMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show(
                    this,
                    Localize(
                        "文本框中没有可复制的内容。",
                        "There is no text to copy."),
                    Localize("没有文本", "No text"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox1.Focus();
                return false;
            }

            try
            {
                ExecuteClipboardOperation(() => Clipboard.SetDataObject(value, true));

                ShowStatus(successMessage);
                textBox1.Focus();
                return true;
            }
            catch (ExternalException)
            {
                ShowClipboardUnavailableMessage();
                return false;
            }
            catch (ThreadStateException)
            {
                ShowStaThreadMessage();
                return false;
            }
        }

        private static T ExecuteClipboardOperation<T>(Func<T> operation)
        {
            for (int attempt = 1; ; attempt++)
            {
                try
                {
                    return operation();
                }
                catch (ExternalException) when (attempt < ClipboardRetryCount)
                {
                    Thread.Sleep(ClipboardRetryDelayMilliseconds);
                }
            }
        }

        private static void ExecuteClipboardOperation(Action operation)
        {
            ExecuteClipboardOperation(
                () =>
                {
                    operation();
                    return true;
                });
        }

        private void ShowClipboardUnavailableMessage()
        {
            MessageBox.Show(
                this,
                Localize(
                    "剪贴板正被其他程序占用，请稍后再试。",
                    "The clipboard is in use by another application. Try again shortly."),
                Localize("剪贴板暂时不可用", "Clipboard unavailable"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            textBox1.Focus();
        }

        private void EnsureClipboardListenerAvailable()
        {
            if (!liveClipboardRadioButton.Checked ||
                clipboardListenerRegistered)
            {
                return;
            }

            initializingPreferences = true;
            try
            {
                startupClipboardRadioButton.Checked = true;
            }
            finally
            {
                initializingPreferences = false;
            }

            SaveClipboardWatchPreference();
            ShowClipboardListenerUnavailableMessage();
        }

        private void ShowClipboardListenerUnavailableMessage()
        {
            MessageBox.Show(
                this,
                Localize(
                    "系统无法注册剪贴板变化通知，程序已回退到“仅启动时导入”模式。",
                    "Windows clipboard change notifications are unavailable. The app has returned to startup-only mode."),
                Localize("无法启用实时监听", "Live monitoring unavailable"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void ShowStaThreadMessage()
        {
            MessageBox.Show(
                this,
                Localize(
                    "当前程序线程不支持剪贴板操作。请确认程序入口 Main 方法带有 [STAThread] 标记。",
                    "This application thread cannot access the clipboard. Ensure Main has the [STAThread] attribute."),
                Localize("无法访问剪贴板", "Cannot access clipboard"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void ShowStatus(string message)
        {
            this.Text = message;
            timer1.Stop();
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void SetEditorText(
            string value,
            bool preserveSelection = true,
            bool synchronizeClipboard = true)
        {
            string newValue = NormalizeLineEndings(value);
            int selectionStart = preserveSelection ? textBox1.SelectionStart : 0;
            int selectionLength = preserveSelection ? textBox1.SelectionLength : 0;

            if (!string.Equals(textBox1.Text, newValue, StringComparison.Ordinal))
            {
                textBox1.Text = newValue;
            }

            selectionStart = Math.Min(selectionStart, textBox1.TextLength);
            selectionLength = Math.Min(
                selectionLength,
                textBox1.TextLength - selectionStart);
            textBox1.Select(selectionStart, selectionLength);

            if (synchronizeClipboard && directClipboardModeRadioButton.Checked)
            {
                TrySynchronizeClipboard(newValue);
            }
        }

        private void UpdateTextStatistics()
        {
            int lineCount = textBox1.TextLength == 0
                ? 0
                : SplitLines(textBox1.Text).Length;
            label1.Text = Localize("字符数：", "Characters: ") +
                textBox1.TextLength.ToString("N0") +
                Localize("    行数：", "    Lines: ") +
                lineCount.ToString("N0");
        }

        private bool TrySynchronizeClipboard(string value)
        {
            try
            {
                ExecuteClipboardOperation(
                    () =>
                    {
                        if (string.IsNullOrEmpty(value))
                        {
                            Clipboard.Clear();
                        }
                        else
                        {
                            Clipboard.SetDataObject(value, true);
                        }
                    });

                ShowStatus(
                    string.IsNullOrEmpty(value)
                        ? Localize(
                            "处理完成，文本框和剪贴板均已清空",
                            "Processing complete; editor and clipboard cleared")
                        : Localize(
                            "处理完成，结果已同步到剪贴板",
                            "Processing complete; clipboard updated"));
                return true;
            }
            catch (ExternalException)
            {
                ShowClipboardUnavailableMessage();
                return false;
            }
            catch (ThreadStateException)
            {
                ShowStaThreadMessage();
                return false;
            }
        }

        private string Localize(string chineseText, string englishText)
        {
            return useEnglish ? englishText : chineseText;
        }

        private string GetIdleWindowTitle()
        {
            return Localize(
                "ClipEditor - 剪贴板文本整理工具 - Alright Peaches Studio 荣誉出品",
                "ClipEditor - A Clipboard Text Toolkit - Made by Alright Peaches Studio");
        }

        private void ApplyLanguage()
        {
            timer1.Stop();
            this.Text = GetIdleWindowTitle();

            brandLabel.Text = "Made by Alright Peaches Studio";
            buttonLanguage.Text = useEnglish ? "简体中文" : "English";
            buttonDeveloperApps.Text = Localize(
                "开发者其他软件和应用",
                "More apps");

            modeGroupBox.Text = Localize("处理模式", "Processing mode");
            regularModeRadioButton.Text = Localize(
                "常规模式（手动复制或点击下方按钮复制）",
                "Normal (manual copy or click button above to copy)");
            directClipboardModeRadioButton.Text = Localize(
                "直接修改剪贴板（处理后自动复制进剪切板）",
                "Direct clipboard (auto-copy after processing)");

            clipboardWatchGroupBox.Text = Localize(
                "剪贴板监听模式",
                "Clipboard monitoring");
            startupClipboardRadioButton.Text = Localize(
                "仅在程序启动时导入剪贴板",
                "Import clipboard at startup only");
            liveClipboardRadioButton.Text = Localize(
                "实时监听（剪贴板变化时自动更新文本框）",
                "Live monitoring (update editor when clipboard changes)");

            button1.Text = Localize("从剪贴板粘贴", "Paste from clipboard");
            button2.Text = Localize("复制全部内容进剪切板", "Copy all to clipboard");
            button4.Text = Localize("复制选中内容进剪切板", "Copy selection part to clipboard");
            button5.Text = Localize("清空剪贴板", "Clear clipboard");
            button3.Text = Localize("最小化", "Minimize");
            button7.Text = Localize("退出", "Exit");

            replaceGroupBox.Text = Localize("查找与替换", "Find and replace");
            label2.Text = Localize("查找：", "Find:");
            label3.Text = Localize("替换为：", "Replace with:");
            buttonFindNext.Text = Localize("查找下一个", "Find next");
            button6.Text = Localize("全部替换", "Replace all");

            toolsGroupBox.Text = Localize("文本处理", "Text processing");
            button8.Text = Localize("去所有空格", "Remove spaces");
            button12.Text = Localize("去空格和换行", "Remove spaces/lines");
            button18.Text = Localize("去每行前空格", "Trim line starts");
            button20.Text = Localize("删除空白行", "Remove blank lines");
            button13.Text = Localize("去行号", "Remove line numbers");
            button9.Text = Localize("去常见小标题", "Remove list prefixes");
            button21.Text = Localize("去数字小标题", "Remove numeric titles");
            button22.Text = Localize("去句首点号 .", "Remove leading dots");
            button19.Text = Localize("去句首连字符 -", "Remove leading dashes");
            button14.Text = Localize("去箭头符号 ->和>>>", "Remove arrows");
            button17.Text = Localize("添加行号", "Add line numbers");
            button23.Text = Localize("合并段内换行", "Join paragraph lines");
            buttonIndent2.Text = Localize("每行行首加 2 空格", "Indent 2 spaces");
            buttonIndent4.Text = Localize("每行行首加 4 空格", "Indent 4 spaces");
            buttonIndent8.Text = Localize("每行行首加 8 空格", "Indent 8 spaces");
            buttonIndent12.Text = Localize("每行行首加 12 空格", "Indent 12 spaces");
            button10.Text = Localize("所有字母大写", "All Letters Uppercase");
            button11.Text = Localize("所有字母小写", "All Letters Lowercase");
            button15.Text = Localize("每词首字母大写", "Title case");
            button16.Text = Localize("清空文本", "Clear editor");

            RefreshHistoryTabTitles();
            SetToolTips();
            UpdateTextStatistics();
        }

        private void SetToolTips()
        {
            toolTip1.SetToolTip(
                buttonLanguage,
                Localize("切换为英文界面", "Switch to Simplified Chinese"));
            toolTip1.SetToolTip(
                buttonDeveloperApps,
                Localize(
                    "用默认浏览器打开 Alright Peaches Studio 的 Steam 页面",
                    "Open the Alright Peaches Studio Steam page in your default browser"));
            toolTip1.SetToolTip(
                regularModeRadioButton,
                Localize(
                    "处理后的结果保留在文本框中，需要时手动复制",
                    "Keep processed text in the editor and copy it manually"));
            toolTip1.SetToolTip(
                directClipboardModeRadioButton,
                Localize(
                    "每次执行文本处理后，自动把完整结果写入剪贴板",
                    "Write the full result to the clipboard after each processing action"));
            toolTip1.SetToolTip(
                startupClipboardRadioButton,
                Localize(
                    "启动程序时读取一次剪贴板，之后不再自动跟随",
                    "Read the clipboard once at startup and do not follow later changes"));
            toolTip1.SetToolTip(
                liveClipboardRadioButton,
                Localize(
                    "剪贴板文字变化后自动更新文本框；剪贴板清空或变为非文本内容时，文本框也会清空",
                    "Update the editor after clipboard text changes; clear it when the clipboard is empty or non-text"));
            toolTip1.SetToolTip(
                textBox2,
                Localize(
                    "输入查找内容后按 Enter，或点击“查找下一个”",
                    "Enter search text, then press Enter or click Find next"));
            toolTip1.SetToolTip(
                historyTabControl,
                Localize(
                    "剪贴板历史仅保留在本次运行中；点击标题切换，点击右侧 × 删除",
                    "Clipboard history lasts for this session only; click the title to switch or the × on the right to delete"));

            System.Windows.Forms.Control[] controls =
            {
                button1, button2, button3, button4, button5, button6, button7,
                buttonFindNext,
                button8, button9, button10, button11, button12, button13,
                button14, button15, button16, button17, button18, button20,
                button21, buttonIndent2, buttonIndent4, buttonIndent8,
                buttonIndent12
            };

            foreach (System.Windows.Forms.Control control in controls)
            {
                toolTip1.SetToolTip(control, control.Text);
            }

            toolTip1.SetToolTip(
                button19,
                Localize("移除每行句首的 - 符号", "Remove a leading - from each line"));
            toolTip1.SetToolTip(
                button22,
                Localize("移除每行句首的 . 符号", "Remove a leading . from each line"));
            toolTip1.SetToolTip(
                button23,
                Localize(
                    "合并段落内的换行，同时保留段落之间的空行",
                    "Join lines inside paragraphs while preserving blank lines between paragraphs"));
        }

        private void LoadLanguagePreference()
        {
            useEnglish = false;

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    string value = key == null
                        ? null
                        : key.GetValue(RegistryLanguageValueName) as string;
                    useEnglish = string.Equals(
                        value,
                        "en",
                        StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception)
            {
                // 注册表不可读时使用默认中文，不影响主功能启动。
                useEnglish = false;
            }
        }

        private bool SaveLanguagePreference()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
                {
                    if (key == null)
                    {
                        return false;
                    }

                    key.SetValue(
                        RegistryLanguageValueName,
                        useEnglish ? "en" : "zh-CN",
                        RegistryValueKind.String);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void LoadModePreference()
        {
            bool useDirectClipboardMode = false;

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    string value = key == null
                        ? null
                        : key.GetValue(RegistryModeValueName) as string;
                    useDirectClipboardMode = string.Equals(
                        value,
                        "direct",
                        StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception)
            {
                // 注册表不可读时回退到常规模式，不影响程序启动。
                useDirectClipboardMode = false;
            }

            directClipboardModeRadioButton.Checked = useDirectClipboardMode;
            regularModeRadioButton.Checked = !useDirectClipboardMode;
        }

        private bool SaveModePreference()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
                {
                    if (key == null)
                    {
                        return false;
                    }

                    key.SetValue(
                        RegistryModeValueName,
                        directClipboardModeRadioButton.Checked
                            ? "direct"
                            : "normal",
                        RegistryValueKind.String);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void LoadClipboardWatchPreference()
        {
            bool useLiveMonitoring = false;

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    string value = key == null
                        ? null
                        : key.GetValue(RegistryClipboardWatchValueName) as string;
                    useLiveMonitoring = string.Equals(
                        value,
                        "live",
                        StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception)
            {
                // 注册表不可读时回退到仅启动时导入模式。
                useLiveMonitoring = false;
            }

            liveClipboardRadioButton.Checked = useLiveMonitoring;
            startupClipboardRadioButton.Checked = !useLiveMonitoring;
        }

        private bool SaveClipboardWatchPreference()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
                {
                    if (key == null)
                    {
                        return false;
                    }

                    key.SetValue(
                        RegistryClipboardWatchValueName,
                        liveClipboardRadioButton.Checked
                            ? "live"
                            : "startup",
                        RegistryValueKind.String);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static int CountOccurrences(string source, string value)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(value))
            {
                return 0;
            }

            int count = 0;
            int searchIndex = 0;
            while ((searchIndex = source.IndexOf(
                value,
                searchIndex,
                StringComparison.Ordinal)) >= 0)
            {
                count++;
                searchIndex += value.Length;
            }

            return count;
        }

        private static string[] SplitLines(string value)
        {
            return Regex.Split(
                value ?? string.Empty,
                @"\r\n|\r|\n|\u0085|\u2028|\u2029");
        }

        private void NormalizeEditorLineEndingsIfNeeded()
        {
            string currentText = textBox1.Text;
            string normalizedText = NormalizeLineEndings(currentText);
            if (string.Equals(
                currentText,
                normalizedText,
                StringComparison.Ordinal))
            {
                return;
            }

            int selectionStart = Math.Min(
                textBox1.SelectionStart,
                currentText.Length);
            int selectionEnd = Math.Min(
                selectionStart + textBox1.SelectionLength,
                currentText.Length);
            int normalizedSelectionStart = NormalizeLineEndings(
                currentText.Substring(0, selectionStart)).Length;
            int normalizedSelectionEnd = NormalizeLineEndings(
                currentText.Substring(0, selectionEnd)).Length;

            normalizingEditorLineEndings = true;
            try
            {
                textBox1.Text = normalizedText;
                normalizedSelectionStart = Math.Min(
                    normalizedSelectionStart,
                    textBox1.TextLength);
                normalizedSelectionEnd = Math.Min(
                    normalizedSelectionEnd,
                    textBox1.TextLength);
                textBox1.Select(
                    normalizedSelectionStart,
                    Math.Max(
                        0,
                        normalizedSelectionEnd - normalizedSelectionStart));
            }
            finally
            {
                normalizingEditorLineEndings = false;
            }
        }

        private static string NormalizeLineEndings(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value ?? string.Empty;
            }

            bool requiresNormalization = false;
            for (int index = 0; index < value.Length; index++)
            {
                char character = value[index];
                if (character == '\r')
                {
                    if (index + 1 < value.Length && value[index + 1] == '\n')
                    {
                        index++;
                    }
                    else
                    {
                        requiresNormalization = true;
                        break;
                    }
                }
                else if (character == '\n' ||
                    character == '\u0085' ||
                    character == '\u2028' ||
                    character == '\u2029')
                {
                    requiresNormalization = true;
                    break;
                }
            }

            if (!requiresNormalization)
            {
                return value;
            }

            StringBuilder normalized = new StringBuilder(value.Length + 16);
            for (int index = 0; index < value.Length; index++)
            {
                char character = value[index];
                if (character == '\r')
                {
                    if (index + 1 < value.Length && value[index + 1] == '\n')
                    {
                        index++;
                    }

                    normalized.Append(Environment.NewLine);
                }
                else if (character == '\n' ||
                    character == '\u0085' ||
                    character == '\u2028' ||
                    character == '\u2029')
                {
                    normalized.Append(Environment.NewLine);
                }
                else
                {
                    normalized.Append(character);
                }
            }

            return normalized.ToString();
        }

        private static void FlushParagraph(
            ICollection<string> outputLines,
            StringBuilder paragraph)
        {
            if (paragraph.Length == 0)
            {
                return;
            }

            outputLines.Add(paragraph.ToString());
            paragraph.Clear();
        }
    }
}
