using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Gameplay
{
    public class ViewGun : MonoBehaviour
    {
        [Header("ref")]
        public Renderer gunRenderer;
        public TMP_Text txtBullet;
        public int bulletCount;
        [Header("Runtime")]
        public CoinType coinType;

        AssetCtrl assetCtrl => GameManager.GetAssetCtrl;
        public ViewGun Init()
        {
            SetBulletCount(0);
            return this;
        }

        public ViewGun SetCoinType(CoinType coinType)
        {
            this.coinType = coinType;

            var material = assetCtrl.GetGunMaterial(coinType);
            SetMaterial(material);
            return this;
        }

        public ViewGun SetBulletCount(int count)
        {
            bulletCount = count;
            txtBullet.text = bulletCount.ToString();
            return this;
        }

        public void AddBullet(int count)
        {
            bulletCount += count;
            txtBullet.text = bulletCount.ToString();
        }

        public void SetMaterial(Material material)
        {
            gunRenderer.sharedMaterial = material;
        }

        public void Shoot()
        {
            if (bulletCount > 0)
            {
                AddBullet(-1);  
            }
        }
    }
}