using Cysharp.Threading.Tasks;
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
        [Header("Runtime")]
        public List<ViewGun> viewGuns = new();
        LevelDataSO curLevel => GameManager.GetLevelCtrl.CurLevel;
        BlockCtrl blockCtrl => GameManager.GetBlockCtrl;
        BulletCtrl bulletCtrl => GameManager.GetBulletCtrl;
        CancellationTokenSource cts = new();
        bool isShooting = false;
        float cTime;
        float timeShot = 0.2f;
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

        public void SpawnGun()
        {
            var lenght = curLevel.coinTypes.Count;
            for (int i = 0; i < lenght; i++)
            {
                var coinType = curLevel.coinTypes[i];
                var gun = Instantiate(viewGun, gunStartPos);
                gun.Init().SetCoinType(coinType);
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
            var lenght = curLevel.coinTypes.Count;
            var curPos = gunStartPos.localPosition;
            var size = gunSize + offset;
            curPos.x = -(lenght * size / 2f) + size / 2f;
            gunStartPos.localPosition = curPos;
        }

        public void AddBullet(CoinType coinType, int count = 1)
        {
            var gun = viewGuns.Find(x => x.coinType == coinType);
            if (gun == null) return;
            gun.AddBullet(count);
        }

        public void Shot()
        {

            var gunHaveBullets = viewGuns.FindAll(x => x.bulletCount > 0);
            if (gunHaveBullets.Count > 0)
            {
                foreach (var gun in gunHaveBullets)
                {
                    var rs = blockCtrl.Shot(gun.coinType);
                    if (rs.Count > 0)
                    {
                        isShooting = true;
                        var gunPos = gun.transform.position;
                        gun.Shoot();
                        bulletCtrl.Shoot(gunPos, rs, async () =>
                        {
                            await UniTask.Delay(200);
                            blockCtrl.Fill();
                            isShooting = false;
                            cTime = 0;
                        },
                        (block) =>
                        {
                            blockCtrl.gridBlocks[block.gridX, block.gridY] = null;
                            Destroy(block.gameObject);
                        });
                        //foreach (var block in rs)
                        //{
                        //    blockCtrl.gridBlocks[block.gridX, block.gridY] = null;
                        //    Destroy(block.gameObject);
                        //}
                        break;
                    }
                }
            }
        }
        #endregion
    }
}