using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "GunAssetSO", menuName = "ScriptableObjects/GunAssetSO", order = 0)]
    public class GunAssetSO : ScriptableObject
    {
        public Material defaultMaterial;
        public List<GunAsset> gunAssets;
    }

    [System.Serializable]
    public class GunAsset
    {
        public CoinType coinType;
        public Material material;
    }
}