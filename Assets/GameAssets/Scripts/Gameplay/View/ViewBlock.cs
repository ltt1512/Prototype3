using Doozy.Runtime.Common;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class BlockPath
    {
        public List<ViewBlock> blocks = new();
    }
    public class ViewBlock : MonoBehaviour
    {

        [Header("Ref")]
        public CoinType coinType;
        public Renderer blockRenderer;
        public Transform posCast;
        public BlockPath Path;
        public int gridX;
        public int gridY;
        AssetCtrl assetCtrl => GameManager.GetAssetCtrl;
        public ViewBlock Init()
        {
            return this;
        }

        public ViewBlock SetCoinType(CoinType coinType)
        {
            this.coinType = coinType;
            var material = assetCtrl.GetBlockMaterial(coinType);
            SetMaterial(material);
            return this;
        }

        public ViewBlock SetGrid(int x, int y)
        {
            this.gridX = x;
            this.gridY = y;
            return this;
        }

        public void SetMaterial(Material material)
        {
            blockRenderer.sharedMaterial = material;
        }

        public void UpdatePos(float sizeX, float sizeY)
        {
            var pos = Vector3.zero;
            pos.x += gridX * sizeX;
            pos.z += gridY * sizeY;
            transform.localPosition = pos;
        }


        #region test
        [Button]
        public void Test()
        {
            Path = FindPaths();

            var first = 0.8f;
            foreach (var block in Path.blocks)
            {
                block.transform.localScale = Vector3.one * first;
                first -= 0.1f;
                first = Mathf.Max(first, 0.2f);
            }
        }
        [Button]
        public void ResetTest()
        {

            foreach (var block in Path?.blocks)
            {
                block.transform.localScale = Vector3.one;
            }
            Path?.blocks.Clear();
        }
        #endregion

        public BlockPath FindPaths()
        {
            List<BlockPath> paths = new();
            var blocks = new List<ViewBlock>();

            var firstPath = new BlockPath();
            firstPath.blocks = new()
            {
                this
            };
            paths.Add(firstPath);
            var directions = new Vector3[] { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
            for (int i = 0; i < paths.Count; ++i)
            {
                var path = paths[i];
                if (path.blocks.Count <= 0) continue;
                while (true)
                {
                    var lastBlock = path.blocks[^1];
                    blocks.Clear();
                    foreach (var dir in directions)
                    {
                        var block = CastBlock(lastBlock.posCast.position, dir);
                        if (block != null && !path.blocks.Contains(block))
                            blocks.Add(block);
                    }

                    if (blocks.Count <= 0)
                        break;

                    if (blocks.Count > 1)
                    {
                        for (int b = 1; b < blocks.Count; b++)
                        {
                            var block = blocks[b];
                            var nextPath = new BlockPath();
                            nextPath.blocks = new List<ViewBlock>(path.blocks);
                            nextPath.blocks.Add(block);
                            paths.Add(nextPath);
                        }
                    }
                    path.blocks.Add(blocks[0]);

                }
            }
            var rs = paths.OrderByDescending(l => l.blocks.Count).FirstOrDefault();
            return rs;
        }

        private ViewBlock CastBlock(Vector3 pos, Vector3 dir)
        {
            var layer = LayerMask.GetMask("Block");
            RaycastHit[] hits = Physics.RaycastAll(pos, dir, 0.5f, layer);
            foreach (var hit in hits)
            {
                var block = hit.collider.GetComponent<ViewBlock>();
                if (block != null && block != this && block.coinType == coinType)
                    return block;
            }
            return null;
        }
    }

}
