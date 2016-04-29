using System;
using System.Windows.Forms;
using CommandBlockEditor.Utils;

namespace CommandBlockEditor.Windows {
    internal partial class EditTileCommandBlock : Form {
        private ListViewItem.ListViewSubItem item;
        private TileCommandBlock tileCommandBlock;

        internal EditTileCommandBlock (ListViewItem.ListViewSubItem item, TileCommandBlock tileCommandBlock) {
            InitializeComponent();
            this.item = item;
            this.tileCommandBlock = tileCommandBlock;
        }

        private void EditTileCommandBlock_Load (object sender, EventArgs e) {
            this.command_textBox.Text = this.tileCommandBlock.command;
        }

        private void cancel_button_Click (object sender, EventArgs e) {
            this.Close();
        }

        private void save_button_Click (object sender, EventArgs e) {
            this.tileCommandBlock.command = this.command_textBox.Text;
            this.item.Text = tileCommandBlock.command;
            this.Close();
        }
    }
}
