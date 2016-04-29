using Substrate.Core;
using Substrate.Nbt;

namespace CommandBlockEditor.Utils {
    internal class TileCommandBlock {
        /// <summary>
        /// 命令方块所在的 X 坐标
        /// </summary>
        internal int x { get; private set; }
        /// <summary>
        /// 命令方块所在的 Y 坐标
        /// </summary>
        internal int y { get; private set; }
        /// <summary>
        /// 命令方块所在的 Z 坐标
        /// </summary>
        internal int z { get; private set; }
        /// <summary>
        /// 命令方块的命令
        /// </summary>
        private string _command;
        /// <summary>
        /// 命令方块的命令
        /// </summary>
        internal string command {
            set { this.edit = true; this._command = value; }
            get { return this._command; }
        }

        /// <summary>
        /// 是否被编辑过命令，编辑后将自动设置为 true，保存后请手动将值设置为 false
        /// </summary>
        internal bool edit { get; set; }
        /// <summary>
        /// 命令方块所属的 Region 文件
        /// </summary>
        internal RegionFile region { get; private set; }
        /// <summary>
        /// 命令方块所属 Chunk 的 X 位置
        /// </summary>
        internal int chunkX { get; private set; }
        /// <summary>
        /// 命令方块所属 Chunk 的 Z 位置
        /// </summary>
        internal int chunkZ { get; private set; }

        internal TileCommandBlock (TagNodeCompound tileEntitie, RegionFile region, int chunkX, int chunkZ) {
            this.x = int.Parse(tileEntitie["x"].ToString());
            this.y = int.Parse(tileEntitie["y"].ToString());
            this.z = int.Parse(tileEntitie["z"].ToString());
            this._command = tileEntitie["Command"].ToString();
            this.edit = false;
            this.region = region;
            this.chunkX = chunkX;
            this.chunkZ = chunkZ;
        }

        public override string ToString () {
            return string.Format("TileCommandBlock(edit: {0}, x: {1}, y: {2}, z: {3}, command: {4})", this.edit, this.x, this.y, this.z, this.command);
        }
    }
}
