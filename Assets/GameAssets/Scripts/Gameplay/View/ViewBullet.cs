using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay
{
    public class ViewBullet : MonoBehaviour
    {

        private void Awake()
        {
            
        }
        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
        public ViewBullet Init()
        {
            return this;
        }    

        public void Shoot(List<ViewBlock> blocks, Action onDoneAll, Action<ViewBlock> onDoneBlock)
        {
            var sq = DOTween.Sequence(this);
            Debug.Log("count " + blocks.Count);
            for(int i = 0; i < blocks.Count; i++) 
            {  
                var block = blocks[i];
                var pos = block.transform.position;
                pos.y += 0.4f;
                pos.z += 0.4f;
                bool isLast = i == blocks.Count - 1;
                sq.Append(transform.DOJump(pos, 2, 1, 0.3f).SetDelay(0.1f).SetTarget(this).OnComplete(()=>
                {
                    onDoneBlock?.Invoke(block);
                    if(isLast)
                    {
                        onDoneAll?.Invoke();
                        Destroy(gameObject);    
                    }
                }));
            }
        }
    }
}