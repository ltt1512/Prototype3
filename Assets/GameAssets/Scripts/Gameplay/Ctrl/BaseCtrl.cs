using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public abstract class BaseCtrl : MonoBehaviour
    {
        public abstract void Init();
        public abstract void StartGame();
        public abstract void Reset();
        public virtual void OnUpdate() { }
    }
}
