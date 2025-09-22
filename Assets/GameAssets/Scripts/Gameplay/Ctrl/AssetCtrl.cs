using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class AssetCtrl : BaseCtrl
    {
        [Header("Ref")]
        public CoinAssetSO coinAssetSO; 
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


        #endregion

    }
}
