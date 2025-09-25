using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class AssetCtrl : BaseCtrl
    {
        [Header("Ref")]
        public CoinAssetSO coinAssetSO;
        public GunAssetSO gunAssetSO;
        public BlockAssetSO blockAssetSO;
        #region public
        public override void Init()
        {
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }

        public Material GetCoinGlassMat(CoinType coinType)
        {
            var asset = coinAssetSO.coinGlassAssets.Find(x => x.coinType == coinType);
            return asset?.material ?? coinAssetSO.defaultGlassMaterial;
        }

        public Material GetCoinWaterMat(CoinType coinType)
        {
            var asset = coinAssetSO.coinWaterAssets.Find(x => x.coinType == coinType);
            return asset?.material ?? coinAssetSO.defaultWaterMaterial;
        }

        public Material GetGunMaterial(CoinType coinType)
        {
            var asset = gunAssetSO.gunAssets.Find(x => x.coinType == coinType);
            return asset?.material ?? gunAssetSO.defaultMaterial;
        }

        public Material GetBlockMaterial(CoinType coinType)
        {
            var asset = blockAssetSO.blockAssets.Find(x => x.coinType == coinType);
            return asset?.material ?? blockAssetSO.defaultMaterial;
        }


        #endregion

    }
}
