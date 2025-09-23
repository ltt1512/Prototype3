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

        public Material GetCoinMaterial(CoinType coinType)
        {
            var asset = coinAssetSO.coinAssets.Find(x => x.coinType == coinType);
            return asset?.material ?? coinAssetSO.defaultMaterial;
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
