using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CoinAssetSO", menuName = "ScriptableObjects/CoinAssetSO", order = 0)]
    public class CoinAssetSO : ScriptableObject
    {
        public Material defaultGlassMaterial;
        public Material defaultWaterMaterial;
        public List<CoinGlassAsset> coinGlassAssets;
        public List<CoinWaterAsset> coinWaterAssets;

        [Button]
        public void ValidateCoinType()
        {
            var coinTypeLenght = Enum.GetValues(typeof(CoinType)).Length;
            for(int i =0; i < coinGlassAssets.Count;i++)
            {
                var coinGlassAsset = coinGlassAssets[i];
                if(i < coinTypeLenght)
                {
                    coinGlassAsset.coinType = (CoinType)i;
                }
            }

            for (int i = 0; i < coinWaterAssets.Count; i++)
            {
                var coinWaterAsset = coinWaterAssets[i];
                if (i < coinTypeLenght)
                {
                    coinWaterAsset.coinType = (CoinType)i;
                }
            }
        }
    }

    [System.Serializable]
    public class CoinGlassAsset
    {
        public CoinType coinType;
        public Material material;
    }

    [System.Serializable]
    public class CoinWaterAsset
    {
        public CoinType coinType;
        public Material material;
    }
}