using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay
{
    public class ViewBigCoin : MonoBehaviour
    {
        [Header("Ref")]
        public MeshRenderer waterRender;
        public Liquid liquid;
        AssetCtrl assetCtrl => GameManager.GetAssetCtrl;
        void OnDestroy()
        {
            DOTween.Kill(this);
        }   

        public ViewBigCoin SetCoinType(CoinType coinType)
        {
            SetCoinMaterial(coinType);
            return this;
        }

        public void SetCoinMaterial(CoinType coinType)
        {
            var mat = assetCtrl.GetCoinWaterMat(coinType);
            waterRender.material = new Material(mat);
        }

        public void ForceLiquid(float forceMinX = 0.5f, float forceMaxX = 0.7f)
        {
            liquid.ForceX(forceMinX, forceMaxX);
        }

        public void AnimInit()
        {
            transform.localScale = Vector3.zero;
            var sq = DOTween.Sequence();

            sq.Append(transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack).SetTarget(this).SetDelay(0.2f));
            sq.Append(transform.DOScale(1f, 0.1f).SetEase(Ease.OutBack).SetTarget(this));
            ForceLiquid();

        }

        public void MoveToGun(ViewGun viewGun)
        {
            var pos = viewGun.transform.position;
            pos.z -= 1.5f;
            transform.DOMove(pos, 0.5f).SetEase(Ease.Linear).SetTarget(this);
        }

        public void UnFill()
        {
            DOVirtual.Float(0.9f, 1.6f, 0.7f, (v) =>
            {
                liquid.fillAmount = v;
            }).SetEase(Ease.Linear).SetTarget(this);
        }

        public void AnimDestroy()
        {
            var sq = DOTween.Sequence();
            sq.Append(transform.DOScale(0, 0.2f).SetEase(Ease.InBack).SetTarget(this));
            sq.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}