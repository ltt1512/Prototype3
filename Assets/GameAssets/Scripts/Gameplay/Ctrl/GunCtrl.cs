using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Gameplay
{
    public class GunCtrl : BaseCtrl
    {
        [Header("Ref")]
        public Transform gunStartPos;
        public ViewGun viewGun;
        public float gunSize = 1f;
        public float offset = 0.3f;
        public int lenght = 6;
        [Header("Runtime")]
        public List<ViewGun> viewGuns = new();
        LevelDataSO curLevel => GameManager.GetLevelCtrl.CurLevel;
        BlockCtrl blockCtrl => GameManager.GetBlockCtrl;
        BulletCtrl bulletCtrl => GameManager.GetBulletCtrl;
        CancellationTokenSource cts = new();
        bool isShooting = false;
        float cTime;
        float timeShot = 0.5f;
        #region public
        public override void Init()
        {
            isShooting = false;
            cts = new();
            cTime = timeShot;
            ClearAllGun();
            SetStartPos();
            SpawnGun();
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
            cts.Cancel();
            cts.Dispose();
        }

        public override void OnUpdate()
        {
            if (!isShooting)
            {
                cTime += Time.deltaTime;
                if (cTime >= timeShot)
                {
                    Shot();
                }
            }
        }

        public void ResetShoot()
        {
            isShooting = false;
            cTime = 0;
        }

        public void SpawnGun()
        {

            for (int i = 0; i < lenght; i++)
            {
                // var coinType = curLevel.coinTypes[i];
                var gun = Instantiate(viewGun, gunStartPos);
                gun.Init();
                var size = gunSize + offset;
                var pos = Vector3.zero;
                pos.x += i * size;
                gun.transform.localPosition = pos;
                viewGuns.Add(gun);
            }
        }

        public void ClearAllGun()
        {
            foreach (var item in viewGuns)
                Destroy(item.gameObject);
            viewGuns.Clear();
        }

        public void SetStartPos()
        {
            var curPos = gunStartPos.localPosition;
            var size = gunSize + offset;
            curPos.x = -(lenght * size / 2f) + size / 2f;
            gunStartPos.localPosition = curPos;
        }

        public void FillBullet(CoinType coinType, ViewBigCoin viewBigCoin)
        {
            var gun = viewGuns.Find(x => x.IsEmpty());
            if (gun == null) return;
            gun.FillBullet(coinType, viewBigCoin);
        }

        public async void Shot()
        {
            var gunHaveBullets = viewGuns.FindAll(x => x.isFullBullet);
            if (gunHaveBullets.Count > 0)
            {
                foreach (var gun in gunHaveBullets)
                {
                    var rs = blockCtrl.Shot(gun.coinType);

                    if (rs.Count > 0)
                    {
                        foreach (var block in rs)
                            block.isTargeted = true;
                        isShooting = true;
                        var fisrtBlock = rs[0];
                        var dir = (fisrtBlock.transform.position - gun.gunPos.position).normalized;
                        float eulerY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                        gun.transform.DORotate(new Vector3(0, eulerY, 0), 0.2f);
                        await UniTask.Delay(200);
                        var gunPos = gun.gunPos.position;
                        gun.Shoot(dir);
                        bulletCtrl.Shoot(gunPos, rs,
                        (block) =>
                        {
                            blockCtrl.gridBlocks[block.gridX, block.gridY] = null;
                            Destroy(block.gameObject);
                        });
                        blockCtrl.canFill = true;
                        //break;
                    }
                }
            }
        }
        #endregion
    }
}