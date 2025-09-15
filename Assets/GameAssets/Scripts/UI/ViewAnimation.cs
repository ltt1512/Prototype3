using Cysharp.Threading.Tasks;
using Doozy.Runtime.Mody;
using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.UIManager.Containers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamepplay.UI
{
    [RequireComponent(typeof(UIView))]
    public class ViewAnimation : MonoBehaviour
    {
        public UIView uiView;

        public UIAnimator[] uiShows;
        public UIAnimator[] uiHides;


        private void Awake()
        {
            uiView = GetComponent<UIView>();
            uiView.OnShowCallback.Event.AddListener(OnShow);
            uiView.OnHideCallback.Event.AddListener(OnHide);

            foreach (var animator in uiShows)
                if (animator != null)
                    animator.ResetToStartValues();
        }

        private void OnDestroy()
        {
            uiView.OnShowCallback.Event.RemoveListener(OnShow);
            uiView.OnHideCallback.Event.RemoveListener(OnHide);
        }

        private void OnShow()
        {
            if (uiShows == null || uiShows.Length == 0)
                return;
            foreach (var animator in uiShows)
            {
                if (animator != null)
                {
                    animator.Play();
                }
            }
        }

        private void OnHide()
        {
            if (uiHides == null || uiHides.Length == 0)
                return;
            foreach (var animator in uiHides)
            {
                if (animator != null)
                {
                    animator.Play();
                }
            }
        }
    }
}

