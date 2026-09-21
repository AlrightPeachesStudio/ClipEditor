namespace ClipEditor
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.brandLabel = new System.Windows.Forms.Label();
            this.buttonDeveloperApps = new System.Windows.Forms.Button();
            this.buttonLanguage = new System.Windows.Forms.Button();
            this.historyTabControl = new System.Windows.Forms.TabControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.modeGroupBox = new System.Windows.Forms.GroupBox();
            this.modeLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.regularModeRadioButton = new System.Windows.Forms.RadioButton();
            this.directClipboardModeRadioButton = new System.Windows.Forms.RadioButton();
            this.automaticProcessingModeRadioButton = new System.Windows.Forms.RadioButton();
            this.clipboardWatchGroupBox = new System.Windows.Forms.GroupBox();
            this.clipboardWatchLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.startupClipboardRadioButton = new System.Windows.Forms.RadioButton();
            this.liveClipboardRadioButton = new System.Windows.Forms.RadioButton();
            this.clipboardPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.replaceGroupBox = new System.Windows.Forms.GroupBox();
            this.replaceLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonFindNext = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.buttonReplaceCurrent = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.toolsGroupBox = new System.Windows.Forms.GroupBox();
            this.toolsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.button8 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.buttonIndent2 = new System.Windows.Forms.Button();
            this.buttonIndent4 = new System.Windows.Forms.Button();
            this.buttonIndent8 = new System.Windows.Forms.Button();
            this.buttonIndent12 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.clipboardUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rootLayout.SuspendLayout();
            this.headerLayout.SuspendLayout();
            this.modeGroupBox.SuspendLayout();
            this.modeLayout.SuspendLayout();
            this.clipboardWatchGroupBox.SuspendLayout();
            this.clipboardWatchLayout.SuspendLayout();
            this.clipboardPanel.SuspendLayout();
            this.replaceGroupBox.SuspendLayout();
            this.replaceLayout.SuspendLayout();
            this.toolsGroupBox.SuspendLayout();
            this.toolsLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.AutoSize = false;
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.headerLayout, 0, 0);
            this.rootLayout.Controls.Add(this.historyTabControl, 0, 1);
            this.rootLayout.Controls.Add(this.textBox1, 0, 2);
            this.rootLayout.Controls.Add(this.modeGroupBox, 0, 3);
            this.rootLayout.Controls.Add(this.clipboardWatchGroupBox, 0, 4);
            this.rootLayout.Controls.Add(this.clipboardPanel, 0, 5);
            this.rootLayout.Controls.Add(this.replaceGroupBox, 0, 6);
            this.rootLayout.Controls.Add(this.toolsGroupBox, 0, 7);
            this.rootLayout.Controls.Add(this.label1, 0, 8);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.Padding = new System.Windows.Forms.Padding(6);
            this.rootLayout.RowCount = 9;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.Absolute, 36F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.rootLayout.Size = new System.Drawing.Size(1050, 800);
            this.rootLayout.TabIndex = 0;
            // 
            // headerLayout
            // 
            this.headerLayout.AutoSize = true;
            this.headerLayout.ColumnCount = 3;
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 100F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.headerLayout.Controls.Add(this.brandLabel, 0, 0);
            this.headerLayout.Controls.Add(this.buttonDeveloperApps, 1, 0);
            this.headerLayout.Controls.Add(this.buttonLanguage, 2, 0);
            this.headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLayout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.headerLayout.Name = "headerLayout";
            this.headerLayout.RowCount = 1;
            this.headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.headerLayout.TabIndex = 0;
            // 
            // brandLabel
            // 
            this.brandLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.brandLabel.AutoSize = true;
            this.brandLabel.Font = new System.Drawing.Font(
                "Microsoft YaHei UI", 12F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.brandLabel.ForeColor = System.Drawing.Color.FromArgb(218, 90, 55);
            this.brandLabel.Margin = new System.Windows.Forms.Padding(2, 0, 8, 0);
            this.brandLabel.Name = "brandLabel";
            this.brandLabel.TabIndex = 0;
            this.brandLabel.Text = "Made by Alright Peaches Studio";
            // 
            // buttonDeveloperApps
            // 
            this.buttonDeveloperApps.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDeveloperApps.AutoSize = true;
            this.buttonDeveloperApps.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.buttonDeveloperApps.MinimumSize = new System.Drawing.Size(145, 30);
            this.buttonDeveloperApps.Name = "buttonDeveloperApps";
            this.buttonDeveloperApps.TabIndex = 1;
            this.buttonDeveloperApps.Text = "开发者其他软件和应用";
            this.buttonDeveloperApps.UseVisualStyleBackColor = true;
            this.buttonDeveloperApps.Click += new System.EventHandler(
                this.buttonDeveloperApps_Click);
            // 
            // buttonLanguage
            // 
            this.buttonLanguage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonLanguage.AutoSize = true;
            this.buttonLanguage.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLanguage.MinimumSize = new System.Drawing.Size(74, 30);
            this.buttonLanguage.Name = "buttonLanguage";
            this.buttonLanguage.TabIndex = 2;
            this.buttonLanguage.Text = "English";
            this.buttonLanguage.UseVisualStyleBackColor = true;
            this.buttonLanguage.Click += new System.EventHandler(
                this.buttonLanguage_Click);
            // 
            // historyTabControl
            // 
            this.historyTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.historyTabControl.HotTrack = true;
            this.historyTabControl.ItemSize = new System.Drawing.Size(170, 26);
            this.historyTabControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.historyTabControl.Name = "historyTabControl";
            this.historyTabControl.ShowToolTips = true;
            this.historyTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.historyTabControl.TabIndex = 1;
            this.historyTabControl.DrawItem +=
                new System.Windows.Forms.DrawItemEventHandler(
                    this.HistoryTabControl_DrawItem);
            this.historyTabControl.MouseDown +=
                new System.Windows.Forms.MouseEventHandler(
                    this.HistoryTabControl_MouseDown);
            this.historyTabControl.MouseLeave +=
                new System.EventHandler(
                    this.HistoryTabControl_MouseLeave);
            this.historyTabControl.MouseMove +=
                new System.Windows.Forms.MouseEventHandler(
                    this.HistoryTabControl_MouseMove);
            this.historyTabControl.MouseUp +=
                new System.Windows.Forms.MouseEventHandler(
                    this.HistoryTabControl_MouseUp);
            this.historyTabControl.Selecting +=
                new System.Windows.Forms.TabControlCancelEventHandler(
                    this.HistoryTabControl_Selecting);
            this.historyTabControl.SelectedIndexChanged +=
                new System.EventHandler(
                    this.HistoryTabControl_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsTab = true;
            this.textBox1.AutoSize = false;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font(
                "Microsoft YaHei UI", 10.5F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(12, 60);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.textBox1.MaxLength = 0;
            this.textBox1.Multiline = true;
            this.textBox1.MinimumSize = new System.Drawing.Size(0, 120);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = false;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(976, 263);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(
                this.TextBox1_TextChanged);
            // 
            // modeGroupBox
            // 
            this.modeGroupBox.AutoSize = true;
            this.modeGroupBox.Controls.Add(this.modeLayout);
            this.modeGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeGroupBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.modeGroupBox.Name = "modeGroupBox";
            this.modeGroupBox.Padding = new System.Windows.Forms.Padding(6, 14, 6, 3);
            this.modeGroupBox.TabIndex = 3;
            this.modeGroupBox.TabStop = false;
            this.modeGroupBox.Text = "处理模式";
            // 
            // modeLayout
            // 
            this.modeLayout.AutoSize = true;
            this.modeLayout.AutoSizeMode =
                System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.modeLayout.Controls.Add(this.regularModeRadioButton);
            this.modeLayout.Controls.Add(this.directClipboardModeRadioButton);
            this.modeLayout.Controls.Add(this.automaticProcessingModeRadioButton);
            this.modeLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeLayout.Margin = new System.Windows.Forms.Padding(0);
            this.modeLayout.Name = "modeLayout";
            this.modeLayout.TabIndex = 0;
            this.modeLayout.WrapContents = true;
            // 
            // regularModeRadioButton
            // 
            this.regularModeRadioButton.AutoSize = true;
            this.regularModeRadioButton.Checked = true;
            this.regularModeRadioButton.Margin = new System.Windows.Forms.Padding(0, 1, 8, 1);
            this.regularModeRadioButton.Name = "regularModeRadioButton";
            this.regularModeRadioButton.TabIndex = 0;
            this.regularModeRadioButton.TabStop = true;
            this.regularModeRadioButton.Text = "常规模式（手动复制或点击下方按钮复制）";
            this.regularModeRadioButton.UseVisualStyleBackColor = true;
            this.regularModeRadioButton.CheckedChanged +=
                new System.EventHandler(
                    this.ProcessingModeRadioButton_CheckedChanged);
            // 
            // directClipboardModeRadioButton
            // 
            this.directClipboardModeRadioButton.AutoSize = true;
            this.directClipboardModeRadioButton.Margin =
                new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.directClipboardModeRadioButton.Name =
                "directClipboardModeRadioButton";
            this.directClipboardModeRadioButton.TabIndex = 1;
            this.directClipboardModeRadioButton.Text =
                "直接修改剪贴板（处理后自动复制进剪切板）";
            this.directClipboardModeRadioButton.UseVisualStyleBackColor = true;
            this.directClipboardModeRadioButton.CheckedChanged +=
                new System.EventHandler(
                    this.ProcessingModeRadioButton_CheckedChanged);
            // 
            // automaticProcessingModeRadioButton
            // 
            this.automaticProcessingModeRadioButton.AutoSize = true;
            this.automaticProcessingModeRadioButton.Margin =
                new System.Windows.Forms.Padding(8, 1, 0, 1);
            this.automaticProcessingModeRadioButton.Name =
                "automaticProcessingModeRadioButton";
            this.automaticProcessingModeRadioButton.TabIndex = 2;
            this.automaticProcessingModeRadioButton.Text =
                "自动处理（复制后按锁定操作自动处理）";
            this.automaticProcessingModeRadioButton.UseVisualStyleBackColor = true;
            this.automaticProcessingModeRadioButton.CheckedChanged +=
                new System.EventHandler(
                    this.ProcessingModeRadioButton_CheckedChanged);
            // 
            // clipboardWatchGroupBox
            // 
            this.clipboardWatchGroupBox.AutoSize = true;
            this.clipboardWatchGroupBox.Controls.Add(this.clipboardWatchLayout);
            this.clipboardWatchGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clipboardWatchGroupBox.Margin =
                new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.clipboardWatchGroupBox.Name = "clipboardWatchGroupBox";
            this.clipboardWatchGroupBox.Padding =
                new System.Windows.Forms.Padding(6, 14, 6, 3);
            this.clipboardWatchGroupBox.TabIndex = 4;
            this.clipboardWatchGroupBox.TabStop = false;
            this.clipboardWatchGroupBox.Text = "剪贴板监听模式";
            // 
            // clipboardWatchLayout
            // 
            this.clipboardWatchLayout.AutoSize = true;
            this.clipboardWatchLayout.AutoSizeMode =
                System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clipboardWatchLayout.Controls.Add(
                this.startupClipboardRadioButton);
            this.clipboardWatchLayout.Controls.Add(
                this.liveClipboardRadioButton);
            this.clipboardWatchLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clipboardWatchLayout.Margin = new System.Windows.Forms.Padding(0);
            this.clipboardWatchLayout.Name = "clipboardWatchLayout";
            this.clipboardWatchLayout.TabIndex = 0;
            this.clipboardWatchLayout.WrapContents = true;
            // 
            // startupClipboardRadioButton
            // 
            this.startupClipboardRadioButton.AutoSize = true;
            this.startupClipboardRadioButton.Checked = true;
            this.startupClipboardRadioButton.Margin =
                new System.Windows.Forms.Padding(0, 1, 8, 1);
            this.startupClipboardRadioButton.Name =
                "startupClipboardRadioButton";
            this.startupClipboardRadioButton.TabIndex = 0;
            this.startupClipboardRadioButton.TabStop = true;
            this.startupClipboardRadioButton.Text =
                "仅在程序启动时导入剪贴板";
            this.startupClipboardRadioButton.UseVisualStyleBackColor = true;
            // 
            // liveClipboardRadioButton
            // 
            this.liveClipboardRadioButton.AutoSize = true;
            this.liveClipboardRadioButton.Margin =
                new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.liveClipboardRadioButton.Name = "liveClipboardRadioButton";
            this.liveClipboardRadioButton.TabIndex = 1;
            this.liveClipboardRadioButton.Text =
                "实时监听（剪贴板变化时自动更新文本框）";
            this.liveClipboardRadioButton.UseVisualStyleBackColor = true;
            this.liveClipboardRadioButton.CheckedChanged +=
                new System.EventHandler(
                    this.LiveClipboardRadioButton_CheckedChanged);
            // 
            // clipboardPanel
            // 
            this.clipboardPanel.AutoSize = true;
            this.clipboardPanel.AutoSizeMode =
                System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clipboardPanel.Controls.Add(this.button1);
            this.clipboardPanel.Controls.Add(this.button2);
            this.clipboardPanel.Controls.Add(this.button4);
            this.clipboardPanel.Controls.Add(this.button5);
            this.clipboardPanel.Controls.Add(this.button3);
            this.clipboardPanel.Controls.Add(this.button7);
            this.clipboardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clipboardPanel.Location = new System.Drawing.Point(12, 387);
            this.clipboardPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.clipboardPanel.Name = "clipboardPanel";
            this.clipboardPanel.Size = new System.Drawing.Size(976, 32);
            this.clipboardPanel.TabIndex = 5;
            this.clipboardPanel.WrapContents = true;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.button1.MinimumSize = new System.Drawing.Size(116, 30);
            this.button1.Name = "button1";
            this.button1.TabIndex = 0;
            this.button1.Text = "从剪贴板粘贴";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.button2.MinimumSize = new System.Drawing.Size(90, 30);
            this.button2.Name = "button2";
            this.button2.TabIndex = 1;
            this.button2.Text = "复制全部内容进剪切板";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.button4.MinimumSize = new System.Drawing.Size(116, 30);
            this.button4.Name = "button4";
            this.button4.TabIndex = 2;
            this.button4.Text = "复制选中内容进剪切板";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click_1);
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.button5.MinimumSize = new System.Drawing.Size(102, 30);
            this.button5.Name = "button5";
            this.button5.TabIndex = 3;
            this.button5.Text = "清空剪贴板";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.button3.MinimumSize = new System.Drawing.Size(72, 30);
            this.button3.Name = "button3";
            this.button3.TabIndex = 4;
            this.button3.Text = "最小化";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button7
            // 
            this.button7.AutoSize = true;
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.MinimumSize = new System.Drawing.Size(72, 30);
            this.button7.Name = "button7";
            this.button7.TabIndex = 5;
            this.button7.Text = "退出";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // replaceGroupBox
            // 
            this.replaceGroupBox.AutoSize = true;
            this.replaceGroupBox.Controls.Add(this.replaceLayout);
            this.replaceGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.replaceGroupBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.replaceGroupBox.Name = "replaceGroupBox";
            this.replaceGroupBox.Padding = new System.Windows.Forms.Padding(6, 16, 6, 3);
            this.replaceGroupBox.TabIndex = 6;
            this.replaceGroupBox.TabStop = false;
            this.replaceGroupBox.Text = "查找与替换";
            // 
            // replaceLayout
            // 
            this.replaceLayout.AutoSize = true;
            this.replaceLayout.ColumnCount = 7;
            this.replaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.replaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 50F));
            this.replaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.replaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.replaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 50F));
            this.replaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.replaceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.replaceLayout.Controls.Add(this.label2, 0, 0);
            this.replaceLayout.Controls.Add(this.textBox2, 1, 0);
            this.replaceLayout.Controls.Add(this.buttonFindNext, 2, 0);
            this.replaceLayout.Controls.Add(this.label3, 3, 0);
            this.replaceLayout.Controls.Add(this.textBox3, 4, 0);
            this.replaceLayout.Controls.Add(this.buttonReplaceCurrent, 5, 0);
            this.replaceLayout.Controls.Add(this.button6, 6, 0);
            this.replaceLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.replaceLayout.Margin = new System.Windows.Forms.Padding(0);
            this.replaceLayout.Name = "replaceLayout";
            this.replaceLayout.RowCount = 1;
            this.replaceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.replaceLayout.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.TabIndex = 0;
            this.label2.Text = "查找：";
            // 
            // textBox2
            // 
            this.textBox2.Anchor =
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            this.textBox2.Margin = new System.Windows.Forms.Padding(0, 2, 6, 2);
            this.textBox2.MinimumSize = new System.Drawing.Size(120, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.TabIndex = 1;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(
                this.TextBox2_KeyDown);
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonFindNext.AutoSize = true;
            this.buttonFindNext.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.buttonFindNext.MinimumSize = new System.Drawing.Size(90, 30);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.TabIndex = 2;
            this.buttonFindNext.Text = "查找下一个";
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(
                this.ButtonFindNext_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.TabIndex = 3;
            this.label3.Text = "替换为：";
            // 
            // textBox3
            // 
            this.textBox3.Anchor =
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            this.textBox3.Margin = new System.Windows.Forms.Padding(0, 2, 6, 2);
            this.textBox3.MinimumSize = new System.Drawing.Size(120, 0);
            this.textBox3.Name = "textBox3";
            this.textBox3.TabIndex = 4;
            // 
            // buttonReplaceCurrent
            // 
            this.buttonReplaceCurrent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReplaceCurrent.AutoSize = true;
            this.buttonReplaceCurrent.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.buttonReplaceCurrent.MinimumSize = new System.Drawing.Size(80, 30);
            this.buttonReplaceCurrent.Name = "buttonReplaceCurrent";
            this.buttonReplaceCurrent.TabIndex = 5;
            this.buttonReplaceCurrent.Text = "替换";
            this.buttonReplaceCurrent.UseVisualStyleBackColor = true;
            this.buttonReplaceCurrent.Click += new System.EventHandler(
                this.ButtonReplaceCurrent_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button6.AutoSize = true;
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.MinimumSize = new System.Drawing.Size(80, 30);
            this.button6.Name = "button6";
            this.button6.TabIndex = 6;
            this.button6.Text = "全部替换";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // toolsGroupBox
            // 
            this.toolsGroupBox.AutoSize = true;
            this.toolsGroupBox.Controls.Add(this.toolsLayout);
            this.toolsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolsGroupBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.toolsGroupBox.Name = "toolsGroupBox";
            this.toolsGroupBox.Padding = new System.Windows.Forms.Padding(6, 16, 6, 3);
            this.toolsGroupBox.TabIndex = 7;
            this.toolsGroupBox.TabStop = false;
            this.toolsGroupBox.Text = "文本处理";
            // 
            // toolsLayout
            // 
            this.toolsLayout.AutoSize = true;
            this.toolsLayout.ColumnCount = 5;
            this.toolsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 20F));
            this.toolsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 20F));
            this.toolsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 20F));
            this.toolsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 20F));
            this.toolsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 20F));
            this.toolsLayout.Controls.Add(this.button8, 0, 0);
            this.toolsLayout.Controls.Add(this.button12, 1, 0);
            this.toolsLayout.Controls.Add(this.button18, 2, 0);
            this.toolsLayout.Controls.Add(this.button20, 3, 0);
            this.toolsLayout.Controls.Add(this.button23, 4, 0);
            this.toolsLayout.Controls.Add(this.button13, 0, 1);
            this.toolsLayout.Controls.Add(this.button9, 1, 1);
            this.toolsLayout.Controls.Add(this.button21, 2, 1);
            this.toolsLayout.Controls.Add(this.button22, 3, 1);
            this.toolsLayout.Controls.Add(this.button19, 4, 1);
            this.toolsLayout.Controls.Add(this.button14, 0, 2);
            this.toolsLayout.Controls.Add(this.button17, 1, 2);
            this.toolsLayout.Controls.Add(this.buttonIndent2, 2, 2);
            this.toolsLayout.Controls.Add(this.buttonIndent4, 3, 2);
            this.toolsLayout.Controls.Add(this.buttonIndent8, 4, 2);
            this.toolsLayout.Controls.Add(this.buttonIndent12, 0, 3);
            this.toolsLayout.Controls.Add(this.button10, 1, 3);
            this.toolsLayout.Controls.Add(this.button11, 2, 3);
            this.toolsLayout.Controls.Add(this.button15, 3, 3);
            this.toolsLayout.Controls.Add(this.button16, 4, 3);
            this.toolsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolsLayout.Margin = new System.Windows.Forms.Padding(0);
            this.toolsLayout.Name = "toolsLayout";
            this.toolsLayout.RowCount = 4;
            this.toolsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.toolsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.toolsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.toolsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(
                System.Windows.Forms.SizeType.AutoSize));
            this.toolsLayout.TabIndex = 0;
            // 
            // button8
            // 
            this.button8.AutoSize = true;
            this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button8.Margin = new System.Windows.Forms.Padding(1);
            this.button8.MinimumSize = new System.Drawing.Size(0, 30);
            this.button8.Name = "button8";
            this.button8.TabIndex = 0;
            this.button8.Text = "去所有空格";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button12
            // 
            this.button12.AutoSize = true;
            this.button12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button12.Margin = new System.Windows.Forms.Padding(1);
            this.button12.MinimumSize = new System.Drawing.Size(0, 30);
            this.button12.Name = "button12";
            this.button12.TabIndex = 1;
            this.button12.Text = "去空格和换行";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button18
            // 
            this.button18.AutoSize = true;
            this.button18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button18.Margin = new System.Windows.Forms.Padding(1);
            this.button18.MinimumSize = new System.Drawing.Size(0, 30);
            this.button18.Name = "button18";
            this.button18.TabIndex = 2;
            this.button18.Text = "去每行前空格";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button20
            // 
            this.button20.AutoSize = true;
            this.button20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button20.Margin = new System.Windows.Forms.Padding(1);
            this.button20.MinimumSize = new System.Drawing.Size(0, 30);
            this.button20.Name = "button20";
            this.button20.TabIndex = 3;
            this.button20.Text = "删除空白行";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button13
            // 
            this.button13.AutoSize = true;
            this.button13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button13.Margin = new System.Windows.Forms.Padding(1);
            this.button13.MinimumSize = new System.Drawing.Size(0, 30);
            this.button13.Name = "button13";
            this.button13.TabIndex = 5;
            this.button13.Text = "去行号";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button9
            // 
            this.button9.AutoSize = true;
            this.button9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button9.Margin = new System.Windows.Forms.Padding(1);
            this.button9.MinimumSize = new System.Drawing.Size(0, 30);
            this.button9.Name = "button9";
            this.button9.TabIndex = 6;
            this.button9.Text = "去常见小标题";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button21
            // 
            this.button21.AutoSize = true;
            this.button21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button21.Margin = new System.Windows.Forms.Padding(1);
            this.button21.MinimumSize = new System.Drawing.Size(0, 30);
            this.button21.Name = "button21";
            this.button21.TabIndex = 7;
            this.button21.Text = "去数字小标题";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button22
            // 
            this.button22.AutoSize = true;
            this.button22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button22.Margin = new System.Windows.Forms.Padding(1);
            this.button22.MinimumSize = new System.Drawing.Size(0, 30);
            this.button22.Name = "button22";
            this.button22.TabIndex = 8;
            this.button22.Text = "去句首点号 .";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button19
            // 
            this.button19.AutoSize = true;
            this.button19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button19.Margin = new System.Windows.Forms.Padding(1);
            this.button19.MinimumSize = new System.Drawing.Size(0, 30);
            this.button19.Name = "button19";
            this.button19.TabIndex = 9;
            this.button19.Text = "去句首连字符 -";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button14
            // 
            this.button14.AutoSize = true;
            this.button14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button14.Margin = new System.Windows.Forms.Padding(1);
            this.button14.MinimumSize = new System.Drawing.Size(0, 30);
            this.button14.Name = "button14";
            this.button14.TabIndex = 10;
            this.button14.Text = "去箭头符号 ->和>>>";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button17
            // 
            this.button17.AutoSize = true;
            this.button17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button17.Margin = new System.Windows.Forms.Padding(1);
            this.button17.MinimumSize = new System.Drawing.Size(0, 30);
            this.button17.Name = "button17";
            this.button17.TabIndex = 11;
            this.button17.Text = "添加行号";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button23
            // 
            this.button23.AutoSize = true;
            this.button23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button23.Margin = new System.Windows.Forms.Padding(1);
            this.button23.MinimumSize = new System.Drawing.Size(0, 30);
            this.button23.Name = "button23";
            this.button23.TabIndex = 4;
            this.button23.Text = "合并段内换行";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // buttonIndent2
            // 
            this.buttonIndent2.AutoSize = true;
            this.buttonIndent2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonIndent2.Margin = new System.Windows.Forms.Padding(1);
            this.buttonIndent2.MinimumSize = new System.Drawing.Size(0, 30);
            this.buttonIndent2.Name = "buttonIndent2";
            this.buttonIndent2.TabIndex = 12;
            this.buttonIndent2.Text = "每行行首加 2 空格";
            this.buttonIndent2.UseVisualStyleBackColor = true;
            this.buttonIndent2.Click += new System.EventHandler(
                this.buttonIndent2_Click);
            // 
            // buttonIndent4
            // 
            this.buttonIndent4.AutoSize = true;
            this.buttonIndent4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonIndent4.Margin = new System.Windows.Forms.Padding(1);
            this.buttonIndent4.MinimumSize = new System.Drawing.Size(0, 30);
            this.buttonIndent4.Name = "buttonIndent4";
            this.buttonIndent4.TabIndex = 13;
            this.buttonIndent4.Text = "每行行首加 4 空格";
            this.buttonIndent4.UseVisualStyleBackColor = true;
            this.buttonIndent4.Click += new System.EventHandler(
                this.buttonIndent4_Click);
            // 
            // buttonIndent8
            // 
            this.buttonIndent8.AutoSize = true;
            this.buttonIndent8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonIndent8.Margin = new System.Windows.Forms.Padding(1);
            this.buttonIndent8.MinimumSize = new System.Drawing.Size(0, 30);
            this.buttonIndent8.Name = "buttonIndent8";
            this.buttonIndent8.TabIndex = 14;
            this.buttonIndent8.Text = "每行行首加 8 空格";
            this.buttonIndent8.UseVisualStyleBackColor = true;
            this.buttonIndent8.Click += new System.EventHandler(
                this.buttonIndent8_Click);
            // 
            // buttonIndent12
            // 
            this.buttonIndent12.AutoSize = true;
            this.buttonIndent12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonIndent12.Margin = new System.Windows.Forms.Padding(1);
            this.buttonIndent12.MinimumSize = new System.Drawing.Size(0, 30);
            this.buttonIndent12.Name = "buttonIndent12";
            this.buttonIndent12.TabIndex = 15;
            this.buttonIndent12.Text = "每行行首加 12 空格";
            this.buttonIndent12.UseVisualStyleBackColor = true;
            this.buttonIndent12.Click += new System.EventHandler(
                this.buttonIndent12_Click);
            // 
            // button10
            // 
            this.button10.AutoSize = true;
            this.button10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button10.Margin = new System.Windows.Forms.Padding(1);
            this.button10.MinimumSize = new System.Drawing.Size(0, 30);
            this.button10.Name = "button10";
            this.button10.TabIndex = 16;
            this.button10.Text = "所有字母大写";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.AutoSize = true;
            this.button11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button11.Margin = new System.Windows.Forms.Padding(1);
            this.button11.MinimumSize = new System.Drawing.Size(0, 30);
            this.button11.Name = "button11";
            this.button11.TabIndex = 17;
            this.button11.Text = "所有字母小写";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button15
            // 
            this.button15.AutoSize = true;
            this.button15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button15.Margin = new System.Windows.Forms.Padding(1);
            this.button15.MinimumSize = new System.Drawing.Size(0, 30);
            this.button15.Name = "button15";
            this.button15.TabIndex = 18;
            this.button15.Text = "每词首字母大写";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.AutoSize = true;
            this.button16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button16.Margin = new System.Windows.Forms.Padding(1);
            this.button16.MinimumSize = new System.Drawing.Size(0, 30);
            this.button16.Name = "button16";
            this.button16.TabIndex = 19;
            this.button16.Text = "清空文本";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.label1.TabIndex = 8;
            this.label1.Text = "字符数：0    行数：0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // clipboardUpdateTimer
            // 
            this.clipboardUpdateTimer.Interval = 180;
            this.clipboardUpdateTimer.Tick += new System.EventHandler(
                this.ClipboardUpdateTimer_Tick);
            // 
            // Form1
            // 
            this.AutoSize = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1050, 800);
            this.Controls.Add(this.rootLayout);
            this.Font = new System.Drawing.Font(
                "Microsoft YaHei UI", 8.25F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(860, 660);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClipEditor - 剪贴板文本整理工具 - Alright Peaches Studio 荣誉出品";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.headerLayout.ResumeLayout(false);
            this.headerLayout.PerformLayout();
            this.modeGroupBox.ResumeLayout(false);
            this.modeGroupBox.PerformLayout();
            this.modeLayout.ResumeLayout(false);
            this.modeLayout.PerformLayout();
            this.clipboardWatchGroupBox.ResumeLayout(false);
            this.clipboardWatchGroupBox.PerformLayout();
            this.clipboardWatchLayout.ResumeLayout(false);
            this.clipboardWatchLayout.PerformLayout();
            this.clipboardPanel.ResumeLayout(false);
            this.clipboardPanel.PerformLayout();
            this.replaceGroupBox.ResumeLayout(false);
            this.replaceGroupBox.PerformLayout();
            this.replaceLayout.ResumeLayout(false);
            this.replaceLayout.PerformLayout();
            this.toolsGroupBox.ResumeLayout(false);
            this.toolsGroupBox.PerformLayout();
            this.toolsLayout.ResumeLayout(false);
            this.toolsLayout.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.TableLayoutPanel headerLayout;
        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.Button buttonDeveloperApps;
        private System.Windows.Forms.Button buttonLanguage;
        private System.Windows.Forms.TabControl historyTabControl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox modeGroupBox;
        private System.Windows.Forms.FlowLayoutPanel modeLayout;
        private System.Windows.Forms.RadioButton regularModeRadioButton;
        private System.Windows.Forms.RadioButton directClipboardModeRadioButton;
        private System.Windows.Forms.RadioButton automaticProcessingModeRadioButton;
        private System.Windows.Forms.GroupBox clipboardWatchGroupBox;
        private System.Windows.Forms.FlowLayoutPanel clipboardWatchLayout;
        private System.Windows.Forms.RadioButton startupClipboardRadioButton;
        private System.Windows.Forms.RadioButton liveClipboardRadioButton;
        private System.Windows.Forms.FlowLayoutPanel clipboardPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox replaceGroupBox;
        private System.Windows.Forms.TableLayoutPanel replaceLayout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonFindNext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button buttonReplaceCurrent;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox toolsGroupBox;
        private System.Windows.Forms.TableLayoutPanel toolsLayout;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button buttonIndent2;
        private System.Windows.Forms.Button buttonIndent4;
        private System.Windows.Forms.Button buttonIndent8;
        private System.Windows.Forms.Button buttonIndent12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer clipboardUpdateTimer;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
