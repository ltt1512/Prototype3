using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class ViewCoin : MonoBehaviour
    {
        [Header("Ref")]
        public MeshRenderer glassRender;
        public MeshRenderer waterRender;
        public Liquid liquip;
        public float forceMinX = 0.5f;
        public float forceMaxX = 0.7f;
        [Header("Runtime")]
        public CoinType type;
        public ViewTube Owner;
        public int idPos;
        AssetCtrl assetCtrl => GameManager.GetAssetCtrl;
        TubeCtrl tubeCtrl => GameManager.GetTubeCtrl;
        CoinCtrl coinCtrl => GameManager.GetCoinCtrl;   

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
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

        public void AnimToPos(Vector3 pos, int offset)
        {
            transform.DOLocalJump(pos, 2, 5, 0.2f).SetTarget(this).SetDelay(offset * 0.05f);
            liquip.ForceX(forceMinX, forceMaxX);
            transform.DOScale(1, 0.1f);
        }

        public void SetCoinMaterial()
        {
            var material = assetCtrl.GetCoinGlassMat(type);
            
            glassRender.material =new Material(material);

            var mat = assetCtrl.GetCoinWaterMat(type);
            waterRender.material = new Material(mat);   
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
            transform.DOScale(1.2f, 0.1f);
            liquip.ForceX(forceMinX, forceMaxX);
        }

        public void AnimDeselect()
        {
            var curPos = transform.localPosition;
            curPos.y = 0;
            transform.localPosition = curPos;
            liquip.ForceX(forceMinX, forceMaxX);
            transform.DOScale(1, 0.1f);
        }

        public async void AnimAppear()
        {
            var sign = Random.value >= 0.5f ? 1 : -1;
            var randomPosX = Random.Range(5,6) * sign;
            var pos = Vector3.zero;
            pos.x = randomPosX;
            transform.localPosition = pos;
            var seq = DOTween.Sequence(this);
            seq.Append(transform.DOLocalJump(coinCtrl.GetPosYByID(idPos), 4, 1, 0.2f).SetTarget(this));
            seq.Append(transform.DOScale(1.2f, 0.05f).SetLoops(2, LoopType.Yoyo).SetTarget(this));
            //var seq = DOTween.Sequence();
            //seq.Append(transform.DOScale(1.2f, 0.15f).SetDelay(Random.Range(0.06f, 0.2f)));
            //seq.Append(transform.DOScale(1, 0.1f));
            liquip.ForceX(forceMinX, forceMaxX);
        }
    }
}
