using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
namespace Gameplay
{
    public class ViewGun : MonoBehaviour
    {
        [Header("ref")]
        public Renderer gunRenderer;
        public Transform gunPos;
        public Liquid liquid;

        [Header("Runtime")]
        public CoinType coinType;
        public bool isFillingBullet;
        public bool isFullBullet;
        public bool isLock;
        AssetCtrl assetCtrl => GameManager.GetAssetCtrl;
        public ViewGun Init()
        {
            isFillingBullet = false;
            isFullBullet = false;
            liquid.fillAmount = 1;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            return this;
        }

        public ViewGun SetCoinType(CoinType coinType)
        {
            this.coinType = coinType;
            var material = assetCtrl.GetGunMaterial(coinType);
            SetMaterial(material);
            return this;
        }

        public async void FillBullet(CoinType coinType, ViewBigCoin viewBigCoin)
        {
            isFillingBullet = true;

            viewBigCoin.MoveToGun(this);  
            await UniTask.Delay(400);
            viewBigCoin.UnFill();   
            SetCoinType(coinType);
            DOVirtual.Float(1, 0, 0.7f, (v) =>
            {
                liquid.fillAmount = v;
            }).SetEase(Ease.Linear).OnComplete(() =>
            {
                isFillingBullet = false;
                isFullBullet = true;
                transform.DORotate(new Vector3(0,0,0), 0.3f);
                viewBigCoin.AnimDestroy();
            });
        }

        public void SetMaterial(Material material)
        {
            gunRenderer.material = new Material(material);
        }

        public void Shoot(Vector3 dir)
        {
            DOVirtual.Float(0, 1.2f, 0.2f, (v) =>
            {
                liquid.fillAmount = v;
            }).SetEase(Ease.Linear).OnComplete(() =>
            {
                isFullBullet = false;
            });
            DOVirtual.DelayedCall(0.75f, () =>
            {
                transform.DORotate(new Vector3(0, 180, 0), 0.3f);
            });
        }

        public bool IsEmpty()
        {
            return !isFillingBullet && !isFullBullet;
        }   
    }
}