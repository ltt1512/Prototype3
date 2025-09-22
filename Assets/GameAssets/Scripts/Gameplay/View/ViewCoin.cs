using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ViewCoin : MonoBehaviour
    {
        [Header("Ref")]
        public MeshRenderer meshRenderer;

        [Header("Runtime")]
        public CoinType type;
        public ViewTube Owner;
        public int idPos;
        AssetCtrl assetCtrl => GameManager.GetAssetCtrl;
        TubeCtrl tubeCtrl => GameManager.GetTubeCtrl;
        public ViewCoin Init()
        {
            return this;
        }

        public ViewCoin SetCoinType(CoinType coinType)
        {
            type = coinType;
            SetCoinMaterial();
            return this;
        }

        public ViewCoin SetOwner(ViewTube owner)
        {
            Owner = owner;
            return this;
        }

        public ViewCoin SetIdPos(int id)
        {
            idPos = id;
            return this;
        }

        public ViewCoin SetLocalPos(Vector3 pos)
        {
            transform.localPosition = pos;  
            return this;
        }

        public void SetCoinMaterial()
        {
            var material = assetCtrl.GetCoinMaterial(type);
            meshRenderer.sharedMaterial = material;
        }

        private void OnMouseDown()
        {
            var coinSelect = tubeCtrl.viewCoinSelects;
            bool isSelected = coinSelect.Count > 0;
            if (isSelected)
            {
                tubeCtrl.DropCoins(Owner);
                return;
            }
            var coins = Owner.GetCoinPack(this);
            if (coins.Count == 0) return;
            var lastCoin = coins[coins.Count - 1];

            foreach (var coin in coins)
            {
                coin.AnimSelect();
            }
        }

        public void AnimSelect()
        {
            var curPos = transform.localPosition;
            curPos.y += 0.2f;
            transform.localPosition = curPos;
        }

        public void AnimDeselect()
        {
            var curPos = transform.localPosition;
            curPos.y = 0;
            transform.localPosition = curPos;
        }
    }
}
