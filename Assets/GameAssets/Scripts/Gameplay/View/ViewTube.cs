using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ViewTube : MonoBehaviour
    {
        [Header("Ref")]
        public Transform coinParent;

        [Header("Runtime")]
        public List<ViewCoin> coins = new();
        TubeCtrl tubeCtrl => GameManager.GetTubeCtrl;
        CoinCtrl coinCtrl => GameManager.GetCoinCtrl;
        LevelCtrl levelCtrl => GameManager.GetLevelCtrl;
        GunCtrl gunCtrl => GameManager.GetGunCtrl;
        public ViewTube Init()
        {
            coins = new();
            return this;
        }

        public ViewTube AddCoin(ViewCoin coin)
        {
            coins.Add(coin);
            return this;
        }

        public List<ViewCoin> GetCoinPack(ViewCoin viewCoin)
        {
            var id = coins.IndexOf(viewCoin);
            return GetCoinPack(id);
        }

        public List<ViewCoin> GetLastCoinPack()
        {
            var rs = new List<ViewCoin>();
            rs.Clear();
            if (coins.Count == 0) return rs;
            var lastCoin = coins[^1];
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                var coin = coins[i];
                if (coin.type == lastCoin.type)
                {
                    if (!rs.Contains(coin))
                        rs.Add(coin);
                }
                else
                    break;
            }
            rs.Sort((a, b) => a.idPos.CompareTo(b.idPos));
            tubeCtrl.viewCoinSelects = rs;
            return rs;
        }

        public List<ViewCoin> GetCoinPack(int id)
        {
            if (id < 0 || id >= coins.Count) return new();
            var curCoin = coins[id];
            var rs = new List<ViewCoin>();
            for (int i = id; i < coins.Count; i++)
            {
                var coin = coins[i];
                if (coin.type == curCoin.type)
                {
                    if (!rs.Contains(coin))
                        rs.Add(coin);
                }
                else
                    break;
            }

            for (int i = id - 1; i >= 0; i--)
            {
                var coin = coins[i];
                if (coin.type == curCoin.type)
                {
                    if (!rs.Contains(coin))
                        rs.Add(coin);
                }
                else
                    break;
            }
            rs.Sort((a, b) => a.idPos.CompareTo(b.idPos));
            if (rs.Count > 0)
            {
                var lastCoin = rs[^1];
                if (lastCoin != coins[^1])
                    rs = GetLastCoinPack();
            }
            tubeCtrl.viewCoinSelects = rs;
            return rs;
        }

        public void Merge()
        {
            if (coins.Count < tubeCtrl.maxCoinInTube) return;
            var lastCoin = coins[^1];
            var coinType = lastCoin.type;
            int total = 0;
            foreach (var coin in coins)
            {
                if (coin.type == coinType)
                    total++;
            }
            bool canMerge = total >= tubeCtrl.maxCoinInTube;    
            if (canMerge)
            {
                //remove old coin
                foreach (var coin in coins)
                    Destroy(coin.gameObject);

                coins.Clear();

                //gun add bullet
                gunCtrl.AddBullet(coinType);
            }
        }

        public void Deal()
        {
            if(coins.Count >= tubeCtrl.maxCoinInTube)
                return;

            var canDeal = UnityEngine.Random.value <= 0.8f; 
            if (canDeal)
            {
                var space = tubeCtrl.maxCoinInTube - coins.Count;

                if (space > 0)
                {
                    var numDeal = UnityEngine.Random.Range(1, space + 1);   
                    int lastIds = coins.Count;
                    for (int i = 0; i < numDeal; i++)
                    {
                        var newCoin = coinCtrl.SpawnCoin(this, lastIds);
                        var coinType = levelCtrl.CurLevel.GetRandom();
                        newCoin.Init().SetCoinType(coinType).SetOwner(this).SetIdPos(coins.Count).AnimAppear();
                        coins.Add(newCoin);
                        lastIds++;
                    }
                }
            }
        }

        private void OnMouseDown()
        {
            var coinSelect = tubeCtrl.viewCoinSelects;
            bool isSelected = coinSelect.Count > 0;
            if (isSelected)
            {
                tubeCtrl.DropCoins(this);
                return;
            }
            var coins = GetLastCoinPack();
            if (coins.Count > 0)
            {
                foreach (var coin in coins)
                {
                    coin.AnimSelect();
                }
            }
        }
    }
}

