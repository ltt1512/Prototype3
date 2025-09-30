using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay
{
    public class BulletCtrl : BaseCtrl
    {
        [Header("ref")]
        public ViewBullet bulletPrefab;

        public ViewBullet SpawnBullet(Vector3 pos)
        {
            var bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            bullet.Init();
            return bullet;  
        }

        public void Shoot(Vector3 startPos, List<ViewBlock> blocks, Action onDoneAll, Action<ViewBlock> onDoneBlock)
        {
            var bullet=  SpawnBullet(startPos);
            bullet.Shoot(blocks, onDoneAll, onDoneBlock);    
        }
        
        public override void Init()
        {
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }
    }

}