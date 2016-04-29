namespace CommandBlockEditor.Windows {
    partial class MainForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent () {
            this.listView = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.x = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.y = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.z = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.保存所有修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭世界 = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.保存修改 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.自动修改命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.AllowColumnReorder = true;
            this.listView.AllowDrop = true;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.x,
            this.y,
            this.z,
            this.command});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(0, 25);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(399, 236);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.listView.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 40;
            // 
            // x
            // 
            this.x.Text = "x";
            this.x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.x.Width = 40;
            // 
            // y
            // 
            this.y.Text = "y";
            this.y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.y.Width = 40;
            // 
            // z
            // 
            this.z.Text = "z";
            this.z.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.z.Width = 40;
            // 
            // command
            // 
            this.command.Text = "command";
            this.command.Width = 80;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存所有修改ToolStripMenuItem,
            this.自动修改命令ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(399, 25);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // 保存所有修改ToolStripMenuItem
            // 
            this.保存所有修改ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭世界,
            this.关闭ToolStripMenuItem,
            this.保存修改});
            this.保存所有修改ToolStripMenuItem.Name = "保存所有修改ToolStripMenuItem";
            this.保存所有修改ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.保存所有修改ToolStripMenuItem.Text = "文件";
            // 
            // 关闭世界
            // 
            this.关闭世界.Enabled = false;
            this.关闭世界.Name = "关闭世界";
            this.关闭世界.Size = new System.Drawing.Size(152, 22);
            this.关闭世界.Text = "关闭世界";
            this.关闭世界.Click += new System.EventHandler(this.关闭世界_Click);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(149, 6);
            // 
            // 保存修改
            // 
            this.保存修改.Enabled = false;
            this.保存修改.Name = "保存修改";
            this.保存修改.Size = new System.Drawing.Size(152, 22);
            this.保存修改.Text = "保存修改";
            this.保存修改.Click += new System.EventHandler(this.保存修改_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "选择 Minecraft 地图文件夹";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // 自动修改命令ToolStripMenuItem
            // 
            this.自动修改命令ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.空ToolStripMenuItem});
            this.自动修改命令ToolStripMenuItem.Name = "自动修改命令ToolStripMenuItem";
            this.自动修改命令ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.自动修改命令ToolStripMenuItem.Text = "自动修改命令";
            // 
            // 空ToolStripMenuItem
            // 
            this.空ToolStripMenuItem.Enabled = false;
            this.空ToolStripMenuItem.Name = "空ToolStripMenuItem";
            this.空ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.空ToolStripMenuItem.Text = "(空)";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(399, 261);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader x;
        private System.Windows.Forms.ColumnHeader y;
        private System.Windows.Forms.ColumnHeader z;
        private System.Windows.Forms.ColumnHeader command;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 保存所有修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭世界;
        private System.Windows.Forms.ToolStripSeparator 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存修改;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem 自动修改命令ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空ToolStripMenuItem;
    }
}

