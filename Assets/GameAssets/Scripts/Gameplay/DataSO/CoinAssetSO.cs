using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CoinAssetSO", menuName = "ScriptableObjects/CoinAssetSO", order = 0)]
    public class CoinAssetSO : ScriptableObject
    {
        public Material defaultMaterial;    
        public List<CoinAsset> coinAssets;
    }

    [System.Serializable]
    public class CoinAsset
    {
        public CoinType coinType;
        public Material material;
    }
}