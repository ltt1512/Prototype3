using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamepplay.UI
{
    public class ViewWin : MonoBehaviour
    {
        public UIButton btnNext;
        public UIButton btnReplay;
        public UIButton btnHome;
        private void Awake()
        {
            btnNext.onClickEvent.AddListener(OnBtnNextClick);
            btnReplay.onClickEvent.AddListener(OnBtnReplayClick);
            btnHome.onClickEvent.AddListener(OnBtnHomeClick);
        }

        private void OnDestroy()
        {
            btnNext.onClickEvent.RemoveListener(OnBtnNextClick);
            btnReplay.onClickEvent.RemoveListener(OnBtnReplayClick);
            btnHome.onClickEvent.RemoveListener(OnBtnHomeClick);
        }

        private void OnBtnNextClick()
        {
            BackButton.Fire();
        }

        private void OnBtnReplayClick()
        {
            BackButton.Fire();
        }

        private void OnBtnHomeClick()
        {
            Signal.Send(StreamId.FlowMainmenu.MainMenu);
        }
    }
}
