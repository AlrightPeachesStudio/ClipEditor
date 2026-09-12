# ClipEditor

**Windows 剪贴板文本整理工具 · A Windows Clipboard Text Toolkit**

Made by **Alright Peaches Studio**

[简体中文](#简体中文) · [English](#english)

## 简体中文

ClipEditor 是使用 C# 和 Windows Forms 开发的 Windows 桌面工具，用于读取、编辑和整理剪贴板中的纯文本。适合处理从聊天工具、网页、文档和代码片段中复制的内容。

### 主要功能

- **可编辑文本框**：支持直接输入、修改、选中复制，以及字符数和行数统计。
- **剪贴板操作**：从剪贴板导入、复制全部、复制选中内容和清空剪贴板。
- **文本整理**：去空格、去空格和换行、去行首空白、删除空白行、合并段落内部换行。
- **列表与符号处理**：移除支持的行号、常见列表前缀、数字小标题、行首点号、行首连字符和箭头符号。
- **格式转换**：添加行号、转换为大写或小写，以及每词首字母大写。
- **行首缩进**：为每个非空行添加 2、4、8 或 12 个普通空格，空行保持为空。
- **独立查找与替换**：查找下一个匹配项，或执行全部替换。
- **临时历史标签**：查看本次运行期间载入的内容，并通过标签右侧的 `×` 删除记录。
- **中英文界面**：一键切换语言，并记住上次选择。
- **两类独立模式设置**：处理模式与剪贴板监听模式均可保存，重启后恢复。
- **多种换行支持**：兼容 Windows 的 CRLF、Linux/macOS 的 LF、旧式 Mac 的 CR，以及部分 Unicode 换行字符；载入显示时统一为 Windows 换行。
- **自适应文本编辑区域**：文本框占满工具区域上方的剩余空间；窗体尺寸受当前屏幕工作区限制，较长文本通过滚动条查看。
- **空白区域拖动窗口**：可在布局面板、分组框和普通说明文字上按住左键移动窗口。文本框、按钮、单选按钮和历史标签保留正常操作，不作为拖动区域，也没有 Alt 拖动快捷操作。

> 本软件是 Windows 应用。支持 Linux/macOS 文本换行格式，不代表可以直接在 Linux/macOS 上运行。

### 下载与运行

1. 打开本仓库的 **Releases** 页面。
2. 如果已有可执行版本，下载对应的 Windows 发行压缩包，而不是 GitHub 自动生成的 `Source code (zip)`。
3. 解压所有文件，运行其中的 `ClipEditor.exe`，并保留发行包附带的依赖和配置文件。
4. 安装该版本发行说明要求的 .NET 或 .NET Framework 运行环境（如果需要）。

若尚未提供可执行发行包，请按照下方步骤从源码编译。

具体运行环境和处理器架构以项目文件及各版本的发行说明为准；不同发布方式可能需要不同运行时，不能只根据开发工具版本推断。

### 使用方法

#### 处理模式

| 模式 | 行为 |
| --- | --- |
| 常规模式 | 执行文本整理操作后，结果保留在文本框中，不自动写回剪贴板。需要时点击“复制全部”或“复制选中内容”。 |
| 直接修改剪贴板模式 | 执行文本整理或替换操作后，自动将完整处理结果写入系统剪贴板。 |

直接修改剪贴板模式并不是“每次键盘输入都自动同步”。手动输入、删除和切换历史标签本身不会自动写回剪贴板。

#### 剪贴板监听模式

| 模式 | 行为 |
| --- | --- |
| 仅启动时导入 | 启动时读取一次剪贴板，之后文本框不会自动跟随系统剪贴板变化。仍可使用导入按钮手动读取。 |
| 实时监听 | 程序运行期间监听剪贴板变化，自动载入新的文本内容。剪贴板清空或变为非文本内容时，编辑区也会清空。 |

处理模式和监听模式互相独立，可以分别选择。实时监听启用期间，外部剪贴板变化可能切换当前显示内容；需要专心编辑时，建议选择“仅启动时导入”。

#### 查找与替换

- 在查找框输入文本，点击“查找下一个”，或在查找框中按 `Enter`。
- 输入替换文本后，点击“全部替换”。替换文本可以为空，用于删除匹配内容。
- 当前查找与替换按精确文本匹配并区分大小写，不是正则表达式搜索。

#### 历史标签

- 载入新的剪贴板文本时建立标签，之前标签中的内容仍可访问。
- 点击标签会把对应内容显示在主文本框中，不会自动复制到剪贴板。
- 历史标签不是只读快照：编辑当前文本框也会更新当前标签保存的内容。
- 点击标签右侧的 `×` 可删除记录；关闭区域与切换区域分离。
- 当前最多保留 **20 个标签**；超过上限时自动移除最早的标签。
- 删除最后一个标签后，会保留一个新的空白标签供继续编辑。
- 历史记录仅存在于本次进程内存中，不写入注册表或历史文件，退出后不恢复。

### 设置与隐私

界面语言、处理模式和监听模式保存在当前用户的注册表位置：

```text
HKEY_CURRENT_USER\Software\Alright Peaches Studio\ClipEditor
```

保存的设置值为 `UiLanguage`、`ProcessingMode` 和 `ClipboardWatchMode`。这一设置机制不用于保存文本或历史标签内容。

当前实现的文本处理在本机进行，不提供文本上传或云端历史功能。点击“开发者其他软件和应用”会调用默认浏览器打开下方 Steam 页面；浏览器和 Steam 有各自的数据处理规则。

注意事项：

- 剪贴板可能包含密码、身份信息或其他敏感内容；开启实时监听前请了解这一行为。
- 直接修改剪贴板模式会覆盖系统剪贴板中的原有内容；清空文本的处理操作也可能同步清空剪贴板。
- 临时历史不是备份，删除标签、超过标签上限或关闭程序可能导致内容无法从本软件中恢复。
- 本软件不提供安全擦除保证；“不保存历史文件”不意味着敏感数据绝不可能出现在系统剪贴板历史、分页文件或其他软件中。
- 文本整理操作不等同于专门的语法分析，处理重要文档或代码前请保留原始副本并核对结果。
- 主文本框未设置固定的字符数量上限，但超长文本和多个历史标签仍会占用内存，并可能影响界面响应速度。

### 从源码编译

开发工具：**Visual Studio Community 2022**。本项目开发过程中使用的版本为 **17.13.5**，但这不表示必须使用完全相同的 IDE 版本。

1. 克隆本仓库，或下载完整源码并解压。
2. 在 Visual Studio Installer 中安装 **“.NET 桌面开发”**工作负载。
3. 用 Visual Studio 打开仓库中的 `.sln` 解决方案。
4. 检查项目属性中的目标框架；如有缺失，安装对应的 .NET SDK 或 .NET Framework 目标包/Developer Pack。
5. 如果项目有 NuGet 依赖，先还原 NuGet 包。
6. 将构建配置切换为 **Release**，选择项目支持的处理器平台。
7. 执行 **生成 → 重新生成解决方案**。
8. 从项目生成目录取出运行文件；如果使用 Visual Studio 的“发布”功能，则使用发布目录的完整输出。

源码应包含 `.csproj`、`.sln`、`.cs`、`.resx` 以及项目引用的资源。不要仅上传代码粘贴文本，也不要把个人配置和编译缓存当作必需源码。

发布可执行文件前，请在独立文件夹中测试，最好再在未安装 Visual Studio 的 Windows 环境中验证。不要假设只复制一个 EXE 就能保留全部依赖。

### 问题反馈与贡献

欢迎通过本仓库的 **Issues** 报告问题，或通过 **Pull Requests** 提交改进。

报告问题时，请提供 Windows 版本、显示缩放比例、软件版本、所选模式、复现步骤，以及不包含隐私信息的示例文本。请勿在公开 Issue、截图或日志中提交真实密码、密钥或敏感剪贴板内容。

### 作者与其他应用

**Made by Alright Peaches Studio**

[在 Steam 查看 Alright Peaches Studio 的其他软件和应用](https://store.steampowered.com/search?term=Alright+Peaches+Studio)

### 许可证

本项目采用 **MIT License**，完整条款见 [LICENSE](LICENSE)。

MIT 允许使用、修改、分发和商业使用；分发软件副本或代码的重要部分时，应保留版权及许可声明。软件按原样提供，不作任何担保。第三方组件和资源如附带独立许可证，应遵守其各自条款。

## English

ClipEditor is a Windows desktop application built with C# and Windows Forms for importing, editing, and cleaning up plain text from the system clipboard. It is useful for text copied from chat tools, websites, documents, and code snippets.

### Features

- An editable text area with character and line counts.
- Import clipboard text, copy all text or a selection, and clear the clipboard.
- Remove spaces, line-start whitespace, blank lines, and supported list prefixes or symbols; join lines within paragraphs.
- Add line numbers, convert letter case, and capitalize the first letter of each word.
- Add 2, 4, 8, or 12 ordinary spaces to each non-empty line; empty lines stay empty.
- Independent find-next and replace-all operations.
- Temporary, editable history tabs with close buttons and a limit of 20 entries.
- Simplified Chinese and English interfaces, with remembered preferences.
- Separate processing and clipboard-monitoring modes.
- Normalize CRLF, LF, CR, and supported Unicode line endings for Windows display.
- An editor that fills the remaining space above the processing controls, with scrollbars for longer text.
- Left-button window dragging from non-interactive surfaces. Text fields, buttons, radio buttons, and history tabs retain their normal behavior; there is no Alt-drag operation.

Supporting Linux/macOS line endings does not make this a Linux/macOS application. The current application is Windows-only.

### Download and run

1. Open this repository's **Releases** page.
2. If a Windows build is available, download its release package, not GitHub's automatically generated `Source code (zip)` archive.
3. Extract the complete package and run `ClipEditor.exe`, keeping its dependencies and configuration files together.
4. Install the .NET or .NET Framework runtime specified by that release, if required.

If no executable release is available, build from source. The actual target framework and architecture are defined by the project configuration and release notes, not by the Visual Studio version alone.

### Modes and editing

| Setting | Behavior |
| --- | --- |
| Normal processing | Processing results stay in the editor until you copy them manually. |
| Direct clipboard processing | Text-processing and replacement operations automatically write the complete result to the clipboard. |
| Startup-only import | Read the clipboard at startup, without automatically following later changes; manual import remains available. |
| Live monitoring | Import new clipboard text while the app is running; clear the editor when the clipboard is empty or contains no readable text. |

Processing and monitoring settings are independent. Direct clipboard processing does not synchronize every keystroke: manual editing and history-tab navigation do not automatically write to the clipboard.

Live monitoring can change the displayed entry when another application changes the clipboard. Choose startup-only import when you need an uninterrupted editing session.

Find-next can be invoked with its button or by pressing `Enter` in the search field. Find and replace use case-sensitive, literal matching, not regular expressions. An empty replacement removes the matching text.

### Temporary history

New imported clipboard text receives a tab, preserving access to earlier entries. Selecting a tab displays its text without automatically copying it. Editing the text also updates the selected entry, so tabs are not immutable snapshots.

The app retains at most **20 tabs**, removing the oldest entry when necessary. Each tab can be deleted with its `×` button. Deleting the last tab creates a new empty editing tab.

History is held only in the application's memory. It is not written to history files or registry settings and is not restored after exit. History is not a backup.

### Preferences and privacy

The following preferences are stored under the current user's registry key:

```text
HKEY_CURRENT_USER\Software\Alright Peaches Studio\ClipEditor
```

Values: `UiLanguage`, `ProcessingMode`, and `ClipboardWatchMode`. Clipboard text is not stored by this preference mechanism.

The current text-processing implementation runs locally and does not provide text uploads or cloud history. The developer-apps button opens the linked Steam page in your default browser; the browser and Steam handle data under their own rules.

Please be aware that:

- Clipboard text may contain passwords or personal information, especially during live monitoring.
- Direct processing overwrites clipboard contents; clearing the editor through a processing action can also clear the clipboard.
- Closing the app, deleting entries, or exceeding the history limit can remove access to text held by this app.
- No secure-erasure guarantee is provided. Text may still exist in Windows clipboard history, paging files, or other applications.
- Text transformations are not specialized parsers. Keep original copies of important documents or code and review the results.
- There is no fixed editor character limit, but large text and multiple history entries can consume memory and reduce responsiveness.

### Build from source

Development tool: **Visual Studio Community 2022**; version **17.13.5** was used during development. An exact IDE-version match is not itself a runtime requirement.

1. Clone the repository or extract the complete source archive.
2. Install the **.NET desktop development** workload with Visual Studio Installer.
3. Open the repository's `.sln` solution.
4. Check the project's target framework and install its SDK or .NET Framework targeting/Developer Pack if missing.
5. Restore NuGet packages if the project uses them.
6. Select **Release** and a supported platform, then rebuild the solution.
7. Use the complete build output, or the publish output if publishing through Visual Studio.

The source distribution should include the solution, project files, C# files, `.resx` files, and referenced resources. Before releasing binaries, test the package outside the development output folder and preferably on Windows without Visual Studio installed. Include all required dependencies instead of assuming that the EXE alone is sufficient.

### Feedback and contributions

Use this repository's **Issues** to report problems and **Pull Requests** to propose improvements. Include the Windows version, display scaling, application version, selected modes, reproduction steps, and a sanitized sample. Do not post real passwords, keys, or private clipboard text in public reports or screenshots.

### Developer

**Made by Alright Peaches Studio**

[More software and apps on Steam](https://store.steampowered.com/search?term=Alright+Peaches+Studio)

### License

This project uses the **MIT License**. See [LICENSE](LICENSE) for the full terms.

Use, modification, distribution, and commercial use are permitted, subject to retaining the required copyright and permission notices. The software is provided without warranty. Third-party components and resources with separate licenses remain subject to those licenses.
