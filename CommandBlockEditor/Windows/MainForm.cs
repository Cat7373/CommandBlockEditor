using System.Collections.Generic;
using System.Windows.Forms;
using CommandBlockEditor.Utils;

// TODO
// 显示命令方块的命令列表(完成)
// 修改某个命令方块的命令(完成)
// 保存修改到文件(完成)
// 根据指定规则(正则)批量自动修改命令
// 按命令排序方便查找
// 列出计分板新建的变量列表并给出删除这些变量的命令
// 列出可能出问题的命令如：gamerule
// 支持命令方块矿车
// 没打开世界的时候禁用关闭世界与保存修改

namespace CommandBlockEditor.Windows {
    internal partial class MainForm : Form {
        private CommandBlockIO io;

        internal MainForm () {
            InitializeComponent();
        }

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
            this.listView.Items.Clear();
            int id = 1;
            foreach (TileCommandBlock commandBlock in commandBlocks) {
                ListViewItem item = this.listView.Items.Add(id++.ToString());
                item.SubItems.Add(commandBlock.x.ToString());
                item.SubItems.Add(commandBlock.y.ToString());
                item.SubItems.Add(commandBlock.z.ToString());
                item.SubItems.Add(commandBlock.command);
            }

            // 自动设置列宽
            listView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            // 启用 listView 的刷新
            this.listView.EndUpdate();

            // 设置菜单
            this.关闭世界.Enabled = true;
            this.保存修改.Enabled = true;
        }

        private void MainForm_DragDrop (object sender, DragEventArgs e) {
            if (this.io != null) {
                关闭世界_Click(null, null);
            }
            this.openFiles((string[]) e.Data.GetData(DataFormats.FileDrop));
        }

        private void MainForm_DragEnter (object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listView_MouseDoubleClick (object sender, MouseEventArgs e) {
            int id = this.listView.SelectedItems[0].Index;
            TileCommandBlock tileCommandBlock = this.io.CommandBlocks[id];
            ListViewItem.ListViewSubItem item = this.listView.Items[id].SubItems[4];
            new EditTileCommandBlock(item, tileCommandBlock).Show();    
        }

        private void 关闭世界_Click (object sender, System.EventArgs e) {
            this.关闭世界.Enabled = false;
            this.保存修改.Enabled = false;

            this.io.Close();
            this.io = null;
            this.listView.Items.Clear();
        }     

        private void 保存修改_Click (object sender, System.EventArgs e) {
            this.io.saveAll();
        }

        // 以下为临时调试代码
        private void MainForm_Load (object sender, System.EventArgs e) {
        }

        private void MainForm_Shown (object sender, System.EventArgs e) {
            // this.openFiles(new string[] { "E:\\项目\\C#\\CommandBlockEditor\\r.0.0.mca" });
        }
    }
}
