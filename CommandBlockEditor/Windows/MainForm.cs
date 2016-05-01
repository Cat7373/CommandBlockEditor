using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CommandBlockEditor.Utils;
using Noesis.Javascript;

// TODO
// 显示命令方块的命令列表(完成)
// 修改某个命令方块的命令(完成)
// 保存修改到文件(完成)
// 通过 JavaScript 脚本批量自动修改命令(完成)
// 按命令排序方便查找
// 筛选器
//   列出计分板新建的变量列表并给出删除这些变量的命令
//   列出可能出问题的命令如：gamerule
// 支持命令方块矿车

namespace CommandBlockEditor.Windows {
    internal partial class MainForm : Form {
        /// <summary>
        /// 命令方块读写工具
        /// </summary>
        private CommandBlockIO io;
        /// <summary>
        /// 自动修改命令的脚本列表
        /// </summary>
        private Dictionary<string, string> replaces = new Dictionary<string, string>();

        internal MainForm () {
            InitializeComponent();
        }

        #region functions

        /// <summary>
        /// 打开 Region 文件
        /// </summary>
        /// <param name="files">要被打开的 Region 文件列表</param>
        private void openFiles (string[] files) {
            // 读取目标文件列表
            this.io = new CommandBlockIO(files);

            // 暂停 listView 的刷新
            this.listView.BeginUpdate();

            // 增加命令列表
            List<TileCommandBlock> commandBlocks = this.io.CommandBlocks;
            int id = 1;
            foreach (TileCommandBlock commandBlock in commandBlocks) {
                ListViewItem item = this.listView.Items.Add(id++.ToString());
                item.SubItems.Add(commandBlock.x.ToString());
                item.SubItems.Add(commandBlock.y.ToString());
                item.SubItems.Add(commandBlock.z.ToString());
                item.SubItems.Add(commandBlock.command);
            }

            // 自动设置列宽
            foreach (ColumnHeader column in listView.Columns) {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            // 启用 listView 的刷新
            this.listView.EndUpdate();

            // 设置菜单
            this.文件.Enabled = true;
            this.自动修改命令.Enabled = true;
        }

        /// <summary>
        /// 关闭已打开的 Region 文件
        /// </summary>
        private void closeFiles () {
            this.文件.Enabled = false;
            this.自动修改命令.Enabled = false;
            this.listView.Items.Clear();

            if (this.io != null) {
                this.io.Dispose();
                this.io = null;
            }
        }

        /// <summary>
        /// 打开编辑命令窗口
        /// </summary>
        /// <param name="item">要被修改的命令方块在 listView 里的 item</param>
        private void editCommand (ListViewItem item) {
            // 命令方块在 this.io.CommandBlocks 中的 index
            int id = int.Parse(item.Text) - 1;
            // 获取目标命令方块
            TileCommandBlock tileCommandBlock = this.io.CommandBlocks[id];
            // 获取命令所在的子 item
            ListViewItem.ListViewSubItem subItem = this.listView.Items[id].SubItems[4];
            // 打开编辑命令窗口
            new EditTileCommandBlock(subItem, tileCommandBlock).Show();
        }

        /// <summary>
        /// 载入自动修改脚本，并创建菜单
        /// </summary>
        // TODO 如果两个脚本 name 相同会崩溃
        // TODO 如果 JS 代码有错误会崩溃
        private void loadReplaces () {
            DirectoryInfo replaces = new DirectoryInfo(Directory.GetCurrentDirectory() + "/replaces");
            using (JavascriptContext context = new JavascriptContext()) {
                context.SetParameter("commands", new string[] { });

                foreach (FileInfo replace in replaces.GetFiles()) {
                    if (replace.Extension.Equals(".js")) {
                        string script = File.ReadAllText(replace.FullName);

                        context.Run(script);
                        string name = context.GetParameter("name") as string;

                        this.replaces.Add(name, script);

                        ToolStripMenuItem menuItem = new ToolStripMenuItem(name);
                        menuItem.Click += this.replace_Click;
                        this.自动修改命令.DropDownItems.Add(menuItem);
                    }
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load (object sender, System.EventArgs e) {
        }

        /// <summary>
        /// 窗口显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown (object sender, System.EventArgs e) {
            this.test();
            this.loadReplaces();
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed (object sender, FormClosedEventArgs e) {
            this.closeFiles();
        }

        /// <summary>
        /// 文件拖放事件，先关闭已打开的文件，然后打开拖放进来的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop (object sender, DragEventArgs e) {
            this.closeFiles();
            this.openFiles((string[]) e.Data.GetData(DataFormats.FileDrop));
        }

        /// <summary>
        /// 拖放开始事件，用于检测拖放目标是否是文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter (object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// 双击 listView 事件，用于打开编辑窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseDoubleClick (object sender, MouseEventArgs e) {
            this.editCommand(this.listView.SelectedItems[0]);
        }

        #endregion

        #region 菜单事件

        private void 关闭世界_Click (object sender, System.EventArgs e) {
            this.closeFiles();
        }

        private void 保存修改_Click (object sender, System.EventArgs e) {
            this.io.saveAll();
        }

        private void replace_Click (object sender, EventArgs e) {
            string script = this.replaces[sender.ToString()];

            // 暂停 listView 的刷新
            this.listView.BeginUpdate();

            List<TileCommandBlock> commandBlocks = this.io.CommandBlocks;
            var items = this.listView.Items;
            string[] commands = new string[items.Count];

            for (int i = 0; i < items.Count; i++) {
                int id = int.Parse(items[i].Text) - 1;
                commands[i] = commandBlocks[id].command;
            }

            object[] result;
            using (JavascriptContext context = new JavascriptContext()) {
                context.SetParameter("commands", commands);
                context.Run(script);
                result = context.GetParameter("commands") as object[];
            }

            for (int i = 0; i < items.Count; i++) {
                string command = result[i].ToString();

                items[i].SubItems[4].Text = command;

                int id = int.Parse(items[i].Text) - 1;
                commandBlocks[id].command = command;
            }

            // 启用 listView 的刷新
            this.listView.EndUpdate();
        }

        #endregion

        // 测试代码
        private void test () {
#if DEBUG
            // this.openFiles(new string[] { "E:\\项目\\C#\\CommandBlockEditor\\r.0.0.mca" });
#endif
        }
    }
}
