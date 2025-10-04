using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay
{
    public class BulletCtrl : BaseCtrl
    {
        [Header("ref")]
        public ViewBullet bulletPrefab;
        public List<ViewBullet> bullets = new();
        public ViewBullet SpawnBullet(Vector3 pos)
        {
            var bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            bullet.Init();
            bullets.Add(bullet);
            return bullet;  
        }

        public void Shoot(Vector3 startPos, List<ViewBlock> blocks, Action<ViewBlock> onDoneBlock)
        {
            var bullet=  SpawnBullet(startPos);
            bullet.Shoot(blocks, onDoneBlock);    
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