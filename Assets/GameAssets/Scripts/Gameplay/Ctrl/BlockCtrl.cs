using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

namespace Gameplay
{
    public class BlockCtrl : BaseCtrl
    {
        [Header("Ref")]
        public ViewBlock viewBlock;
        public Transform blockStartPos;
        public Vector3Int grid = new(6, 1, 7);
        public Vector3 cellSize = new(0.5f, 0.5f, 0.5f);
        public float offset;

        [Header("Runtime")]
        // public List<ViewBlock> viewBlocks = new();
        public ViewBlock[,] gridBlocks;
        LevelDataSO curLevel => GameManager.GetLevelCtrl.CurLevel;
        BulletCtrl bulletCtrl => GameManager.GetBulletCtrl;
        GunCtrl gunCtrl => GameManager.GetGunCtrl;
        public bool canFill = false;
        public override void Init()
        {
            canFill = false;
            ClearAllBlock();
            SetBlockStartPos();
            SpawnBlock();
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }

        override public void OnUpdate()
        {
            if (canFill && bulletCtrl.bullets.Count <= 0)
            {
                Fill();
                canFill = false;
                gunCtrl.ResetShoot();   
            }
        }

        public async void SpawnBlock()
        {
            gridBlocks = new ViewBlock[grid.x, grid.z];
            var sizeX = cellSize.x + offset;
            var sizeZ = cellSize.z + offset;
            for (int z = 0; z < grid.z; z++)
            {
                for (int x = 0; x < grid.x; x++)
                {
                    var block = Instantiate(viewBlock, blockStartPos);
                    var coinType = curLevel.GetRandom();
                    block.Init().SetCoinType(coinType).SetGrid(x, z).UpdatePos(sizeX, sizeZ);
                    block.ActiveObi(false);
                    block.AnimShow(0.2f, 0.5f);
                    gridBlocks[x, z] = block;
                    await UniTask.Delay(100);

                }
            }
        }


        public void SetBlockStartPos()
        {
            var curPos = blockStartPos.localPosition;
            var size = cellSize.x + offset;
            curPos.x = -(grid.x * size / 2f) + size / 2f;
            blockStartPos.localPosition = curPos;
        }

        public void ClearAllBlock()
        {
            if (gridBlocks == null) return;
            int sizeX = gridBlocks.GetLength(0);
            int sizeY = gridBlocks.GetLength(1);

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    var block = gridBlocks[x, y];
                    if (block != null)
                        Destroy(block.gameObject);
                }
            }
            gridBlocks = null;
        }

        public List<ViewBlock> Shot(CoinType type)
        {
            var lstFistRow = new List<ViewBlock>();
            for (int x = 0; x < grid.x; x++)
            {
                if (gridBlocks[x, 0] != null && !gridBlocks[x, 0].isTargeted)
                    lstFistRow.Add(gridBlocks[x, 0]);
            }

            var firstBlock = lstFistRow.Find(x => x.coinType == type);
            if (firstBlock == null) return new();

            var blocks = firstBlock.FindPaths().blocks;
            //foreach (var block in blocks)
            //{
            //    gridBlocks[block.gridX, block.gridY] = null;
            //    Destroy(block.gameObject);
            //}
            return blocks;
        }
        [Button]
        public void Fill()
        {
            int lengthX = gridBlocks.GetLength(0);
            int lengthY = gridBlocks.GetLength(1);
            var sizeX = cellSize.x + offset;
            var sizeZ = cellSize.z + offset;
            List<int> rowNeedFill = new List<int>();
            for (int x = 0; x < lengthX; x++)
            {
                for (int y = 0; y < lengthY; y++)
                {
                    var block = gridBlocks[x, y];
                    if (block == null)
                    {
                        rowNeedFill.Add(x);
                        break;
                    }
                }
            }
            var blocks = new List<ViewBlock>();
            foreach (var id in rowNeedFill)
            {
                blocks.Clear();
                for(int y =  0; y < lengthY; y++)
                {
                    var block = gridBlocks[id, y];
                    if(block != null)
                        blocks.Add(block);
                }
                int newId = 0;
                foreach(var block in blocks)
                {
                    block.gridY = newId;
                    block.UpdatePosByAnim(sizeX, sizeZ);
                    gridBlocks[id, newId] = block;
                    newId++;
                }

                var space = grid.z - blocks.Count;
                for(int s = 0; s < space; s++)
                {
                    var newBlock = Instantiate(viewBlock, blockStartPos);
                    var coinType = curLevel.GetRandom();
                    newBlock.Init().SetCoinType(coinType).SetGrid(id, newId).UpdatePos(sizeX, sizeZ);
                    newBlock.ActiveObi(false);
                    newBlock.AnimShow(0.2f, 0.5f);
                    gridBlocks[id, newId] = newBlock;
                    newId++;
                }
            }
        }
    }

}
