using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay
{
    [CreateAssetMenu(fileName = "BlockAssetSO", menuName = "ScriptableObjects/BlockAssetSO", order = 0)]
    public class BlockAssetSO : ScriptableObject
    {
        public Material defaultMaterial;
        public List<BlockAsset> blockAssets;
    }

    [System.Serializable]
    public class BlockAsset
    {
        public CoinType coinType;
        public Material material;
    }
}
