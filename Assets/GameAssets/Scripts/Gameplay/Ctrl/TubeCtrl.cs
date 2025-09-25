using Sirenix.OdinInspector;
using StansAssets.Foundation.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{

    public class TubeCtrl : BaseCtrl
    {
        [Header("Ref")]
        public ViewTube tubePrefab;
        public Transform startTubePos;
        public Vector3Int grid;
        public Vector3 cellSize;
        public int maxCoinInTube = 4;
        [Header("Runtime")]
        public List<ViewTube> viewTubes = new();
        public List<ViewCoin> viewCoinSelects = new();
        LevelDataSO curLevel => GameManager.GetLevelCtrl.CurLevel;
        CoinCtrl coinCtrl => GameManager.GetCoinCtrl;

        private void Awake()
        {
            StaticBus<EventMerge>.Subscribe(OnEventMerge);
            StaticBus<EventDeal>.Subscribe(OnEventDeal);
        }

        void OnDestroy()
        {
            StaticBus<EventMerge>.Unsubscribe(OnEventMerge);
            StaticBus<EventDeal>.Unsubscribe(OnEventDeal);
        }

       

        private void OnEventMerge(EventMerge merge)
        {
            MergeTube();
        }

        private void OnEventDeal(EventDeal deal)
        {
            DealTube();
        }
        #region public
        public override void Init()
        {
            ClearAllTube();
            SpawnTubes();
        }

        public override void Reset()
        {
            ClearAllTube();
        }

        public override void StartGame()
        {
        }

        public void DropCoins(ViewTube tubeTarget)
        {
            //check type empty
            bool isTubeEmpty = tubeTarget.coins.Count == 0;
            var tubeSelect = viewCoinSelects[0].Owner;
            if (isTubeEmpty)
            {
                var firstPosId = 0;
                
                foreach (var coin in viewCoinSelects)
                {
                    coin.transform.SetParent(tubeTarget.coinParent);
                    Vector3 pos = coinCtrl.GetPosYByID(firstPosId);
                    coin.AnimToPos(pos, firstPosId);
                    coin.SetOwner(tubeTarget).SetIdPos(firstPosId);
                    tubeTarget.AddCoin(coin);
                    tubeSelect.coins.Remove(coin);
                    firstPosId++;

                }
                viewCoinSelects.Clear();
                return;
            }

            var firstCoinSelect = viewCoinSelects[0];
            var selectType = firstCoinSelect.type;
            var lastCoinTarget = tubeTarget.coins[^1];
            var targetType = lastCoinTarget.type;
            //check same tube
            if (tubeSelect == tubeTarget)
            {
                ResetSelect();
                return;
            }

            //check same type
            if (selectType != targetType)
            {
                ResetSelect();
                return;
            }
            int spaceLeft = maxCoinInTube - tubeTarget.coins.Count;
            if (spaceLeft <= 0)
            {
                ResetSelect();
                return;
            }

            int startId = tubeTarget.coins.Count;
            int lastPosId = tubeTarget.coins[^1].idPos;
            lastPosId++;

            var lstViewCoint = new List<ViewCoin>();

            for (int i = viewCoinSelects.Count - 1; i >= 0; --i)
            {
                lstViewCoint.Add(viewCoinSelects[i]);
                spaceLeft--;
                if (spaceLeft <= 0) break;
            }
            lstViewCoint.Reverse();
            int offset = 0;
            foreach(var coin in lstViewCoint)
            {
                coin.transform.SetParent(tubeTarget.coinParent);
                Vector3 pos = coinCtrl.GetPosYByID(lastPosId);
                coin.AnimToPos(pos, offset++);
                coin.SetOwner(tubeTarget).SetIdPos(lastPosId);
                tubeTarget.AddCoin(coin);
                tubeSelect.coins.Remove(coin);
                lastPosId++;
            }
            //get number of empty tube
            foreach (var coin in viewCoinSelects)
                coin.AnimDeselect();
            viewCoinSelects.Clear();


            void ResetSelect()
            {
                foreach (var coin in viewCoinSelects)
                    coin.AnimDeselect();
                viewCoinSelects.Clear();
            }
        }

        public void MergeTube()
        {
            foreach (var tube in viewTubes)
            {
                tube.Merge();
            }
        }

        public void DealTube()
        {
            foreach (var tube in viewTubes)
            {
                tube.Deal();
            }
        }
        #endregion
        #region private
        private void SpawnTubes()
        {
            if (curLevel == null) return;
            for (int z = 0; z < grid.z; z++)
            {
                for (int x = 0; x < grid.x; x++)
                {
                    var pos = Vector3.zero;
                    pos.x += x * cellSize.x;
                    pos.z += z * cellSize.z;
                    var tube = Instantiate(tubePrefab, pos, Quaternion.identity, startTubePos);
                    tube.transform.localPosition = pos;
                    tube.Init();
                    viewTubes.Add(tube);
                }
            }

            var tubeDatas = curLevel.tubeDatas;
            for (int i = 0; i < tubeDatas.Count; i++)
            {
                var tubeData = tubeDatas[i];
                if (i >= viewTubes.Count) break;
                var coinDatas = tubeData.coinDatas;
                var viewTube = viewTubes[i];
                int curCointId = 0;
                foreach (var coinData in coinDatas)
                {
                    for (int j = 0; j < coinData.count; j++)
                    {
                        var coin = coinCtrl.SpawnCoin(viewTube, curCointId + j);
                        coin.Init().SetCoinType(coinData.coinType).SetOwner(viewTube)
                        .SetIdPos(curCointId + j);
                        viewTube.AddCoin(coin);
                    }
                    curCointId += coinData.count;
                }
            }
        }
        private void ClearAllTube()
        {
            foreach (var tube in viewTubes)
                Destroy(tube.gameObject);
            viewTubes.Clear();
        }
        #endregion

        #region help methods
        [Button]
        public void SetStartPos()
        {
            var curPos = startTubePos.localPosition;
            curPos.x = -(grid.x * cellSize.x / 2f) + cellSize.x / 2f;
            startTubePos.localPosition = curPos;
        }
        #endregion
    }
}