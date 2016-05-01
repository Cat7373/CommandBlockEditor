using System.Collections.Generic;
using Substrate.Core;
using Substrate.Nbt;

namespace CommandBlockEditor.Utils {
    /// <summary>
    /// 命令方块编辑工具类
    /// </summary>
    internal class CommandBlockIO {
        /// <summary>
        /// 命令方块列表，外部代码请勿直接增删内容，只能获取某个命令方块后修改其属性
        /// </summary>
        internal List<TileCommandBlock> CommandBlocks { get; private set; } = new List<TileCommandBlock>();

        /// <summary>
        /// 已经打开的 region 文件列表
        /// </summary>
        private List<RegionFile> regions = new List<RegionFile>();

        /// <summary>
        /// 通过 Region 文件列表构造命令方块编辑工具类的实例
        /// </summary>
        /// <param name="files">Region 文件列表</param>
        internal CommandBlockIO (string[] files) {
            // 遍历文件列表
            foreach (var file in files) {
                // 打开 Region 文件 // TODO 非 Region 文件可能抛异常
                var region = new RegionFile(file);
                // 添加到 Region 列表
                this.regions.Add(region);

                // 遍历 Chunk 列表
                for (var chunkX = 0; chunkX < 32; chunkX++) {
                    for (var chunkZ = 0; chunkZ < 32; chunkZ++) {
                        if (region.HasChunk(chunkX, chunkZ)) {
                            var tree = new NbtTree();
                            tree.ReadFrom(region.GetChunkDataInputStream(chunkX, chunkZ));

                            // Level
                            var level = tree.Root["Level"] as TagNodeCompound;
                            // TileEntities
                            var tileEntities = level["TileEntities"] as TagNodeList;
                            // 遍历 TileEntity 列表
                            foreach (TagNodeCompound tileEntity in tileEntities) {
                                // 如果是 CommandBlock
                                if (tileEntity["id"].ToString().Equals("Control")) {
                                    // 加入 CommandBlock 列表
                                    this.CommandBlocks.Add(new TileCommandBlock(tileEntity, region, chunkX, chunkZ));

                                    // 输出调试信息
                                    // System.Diagnostics.Debug.WriteLine("Add" + this.commandBlocks[this.commandBlocks.Count - 1]);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存所有修改到 Region 文件
        /// </summary>
        internal void saveAll () {
            // 缓存变量
            RegionFile lastRegion = null;
            NbtTree lastTree = null;
            TagNodeList lastTileEntities = null;
            int lastChunkX = -1;
            int lastChunkZ = -1;

            // 遍历命令方块列表
            foreach (var commandBlock in this.CommandBlocks) {
                // 如果需要保存
                if (commandBlock.edit) {
                    // 读取目标区块的 TileEntities
                    if (lastRegion == commandBlock.region && commandBlock.chunkX == lastChunkX && commandBlock.chunkZ == lastChunkZ) {
                        // 如果 region chunk 都没变 则触发缓存 继续用上次的 TileEntities
                    } else {
                        if (lastRegion != null) {
                            // 保存之前的修改到文件
                            using (var stream = lastRegion.GetChunkDataOutputStream(lastChunkX, lastChunkZ)) {
                                lastTree.WriteTo(stream);
                            }
                        }

                        // 打开 Region 文件
                        lastRegion = commandBlock.region;
                        // 打开目标 Chunk
                        lastTree = new NbtTree();
                        lastTree.ReadFrom(lastRegion.GetChunkDataInputStream(commandBlock.chunkX, commandBlock.chunkZ));

                        // Level
                        var level = lastTree.Root["Level"] as TagNodeCompound;
                        // TileEntities
                        lastTileEntities = level["TileEntities"] as TagNodeList;

                        // 保存区块位置
                        lastChunkX = commandBlock.chunkX;
                        lastChunkZ = commandBlock.chunkZ;
                    }

                    // 遍历 TileEntity 列表
                    foreach (TagNodeCompound tileEntity in lastTileEntities) {
                        // 当前 TileEntity 的坐标
                        int x = int.Parse(tileEntity["x"].ToString());
                        int y = int.Parse(tileEntity["y"].ToString());
                        int z = int.Parse(tileEntity["z"].ToString());

                        // 如果坐标一致
                        if (x == commandBlock.x && y == commandBlock.y && z == commandBlock.z) {
                            // 修改 Command
                            tileEntity["Command"] = new TagNodeString(commandBlock.command);

                            // 输出调试信息
                            // System.Diagnostics.Debug.WriteLine("Save" + commandBlock);

                            // 设置 CommandBlock 的保存标记
                            commandBlock.edit = false;
                            break;
                        }
                    }
                }
            }

            if (lastRegion != null) {
                // 保存之前的修改到文件
                using (var str = lastRegion.GetChunkDataOutputStream(lastChunkX, lastChunkZ)) {
                    lastTree.WriteTo(str);
                }
            }
        }

        /// <summary>
        /// 销毁本对象，关闭所有已打开的文件
        /// </summary>
        internal void Dispose () {
            foreach (var region in this.regions) {
                region.Dispose();
            }
        }
    }
}
