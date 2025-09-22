using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public enum CoinType
    {
        Coin1,
        Coin2,
        Coin3,
        Coin4,
        Coin5,
        Coin6,
        Coin7,
        Coin8,
        Coin9,
        Coin10
    }

    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataSO", order = 1)]
    public class LevelDataSO : ScriptableObject
    {
        public List<TubeData> tubeDatas = new();
        public List<CoinType> coinTypes = new();
        public void ValidateData()
        {

        }

        public CoinType GetRandom()
        {
            if (coinTypes.Count == 0) return CoinType.Coin1;
            int idx = Random.Range(0, coinTypes.Count);
            return coinTypes[idx];
        }
    }

    [System.Serializable]
    public class CoinData
    {
        public CoinType coinType;
        public int count;
    }

    [System.Serializable]
    public class TubeData
    {
        public List<CoinData> coinDatas = new();
    }
}
