using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        CancellationTokenSource cts = new();
        #region public
        public override void Init()
        {
            cts = new();
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

        public async void Shot(CoinType coinType, int count = 1)
        {
            var gun = viewGuns.Find(x => x.coinType == coinType);
            if (gun != null)
            {
                gun.AddBullet(count);
                var cancel = await UniTask.Delay(300, cancellationToken:cts.Token).SuppressCancellationThrow();
                if (cancel) return;
                gun.Shoot();
                blockCtrl.Shot(coinType);
                cancel = await UniTask.Delay(300, cancellationToken: cts.Token).SuppressCancellationThrow();
                if (cancel) return;
                blockCtrl.Fill();
            }
        }
        #endregion
    }
}