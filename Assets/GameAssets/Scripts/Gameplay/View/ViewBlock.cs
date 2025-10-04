using DG.Tweening;
using Doozy.Runtime.Common;
using Obi;
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
        public List<Renderer> flowerRenderers;
        public Transform posCast;
        public BlockPath Path;
        public int gridX;
        public int gridY;
        public List<ObiCloth> cloths;
        public List<ObiClothRenderer> clothRenderers;
        public Transform parent;
        AssetCtrl assetCtrl => GameManager.GetAssetCtrl;
        public bool isTargeted = false;
        public ViewBlock Init()
        {
            isTargeted = false;
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

        public ViewBlock AnimShow(float timeScale, float timeRotate)
        {
            var curRot = parent.eulerAngles;
            curRot.y += 180;
            var sq = DOTween.Sequence();
            float originScale = 1.5f;
            var originPos = parent.localPosition;
            var localPos = parent.localPosition;
            localPos.y += 6f;
            localPos.x -= 1f;
            parent.localPosition = localPos;
            foreach (var c in cloths)
            {
                c.enabled = true;
                c.AddTorque(new Vector3(0, 0, 100), ForceMode.VelocityChange);
            }
            sq.Append(parent.DORotate(curRot, 0.75f, RotateMode.FastBeyond360).SetEase(Ease.Linear));
            sq.Join(parent.DOLocalMove(originPos, 0.4f).OnComplete(()=>
            {

            }));
            sq.Join(DOVirtual.Float(originScale, 2f, 0.3f, (v) =>
            {
                foreach (var c in cloths)
                    c.stretchingScale = v;
            }).SetLoops(2, LoopType.Yoyo));
            sq.AppendInterval(1f);
            sq.AppendCallback(() =>
            {
                foreach (var c in cloths)
                    c.enabled = false;
            });
            return this;
        }

        public void ActiveObi(bool isActive)
        {
            foreach (var cloth in cloths)
            {
                cloth.enabled = isActive;
            }
        }

        public void SetMaterial(Material material)
        {
            foreach (var r in flowerRenderers)
            {
                r.sharedMaterial = material;
            }
        }

        public void UpdatePos(float sizeX, float sizeY)
        {
            var pos = GetPos(sizeX, sizeY);
            transform.localPosition = pos;
        }

        public void UpdatePosByAnim(float sizeX, float sizeY)
        {
            var pos = GetPos(sizeX, sizeY);
            transform.DOLocalMove(pos, 0.3f);
            foreach (var c in cloths)
            {
                c.enabled = true;
                c.AddTorque(new Vector3(0, 0, Random.Range(40,60)), ForceMode.VelocityChange);
            }
            DOVirtual.DelayedCall(0.75f, () =>
            {
                foreach (var c in cloths)
                    c.enabled = false;
            });
        }

        public Vector3 GetPos(float sizeX, float sizeY)
        {
            var pos = Vector3.zero;
            pos.x += gridX * sizeX;
            pos.z += gridY * sizeY;
            return pos; 
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
                        if (block != null && !path.blocks.Contains(block) && !block.isTargeted)
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
            RaycastHit[] hits = Physics.RaycastAll(pos, dir, 1.1f, layer);
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
