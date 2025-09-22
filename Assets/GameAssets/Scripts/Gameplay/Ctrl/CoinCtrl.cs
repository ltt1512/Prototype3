using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CoinCtrl : BaseCtrl
    {
        [Header("Ref")]
        public ViewCoin coinPrefab;
        public float size = 0.5f;
     


        #region public
        public override void Init()
        {
        }

        public override void Reset()
        {
        }

        public override void StartGame()
        {
        }

        public ViewCoin SpawnCoin(ViewTube viewTube, int id)
        {
            ViewCoin newCoin = Instantiate(coinPrefab, viewTube.coinParent);
            newCoin.transform.localPosition = GetPosYByID(id);
            return newCoin;
        }

        public Vector3 GetPosYByID(int id)
        {
            var pos = Vector3.zero;
            pos.z += size * 3 - size * id;
            return pos;
        }

      
        #endregion

        #region private


        #endregion
    }
}