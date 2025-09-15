using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace Gamepplay.UI
{
    public class ViewMainmenu : MonoBehaviour
    {
        public UIButton btnSetting;
        public UIButton btnPlay;
        private void Awake()
        {
            btnSetting.onClickEvent.AddListener(OnBtnSettingClick);
            btnPlay.onClickEvent.AddListener(OnBtnPlayClick);
        }
        private void OnDestroy()
        {
            btnSetting.onClickEvent.RemoveListener(OnBtnSettingClick);
            btnPlay.onClickEvent.RemoveListener(OnBtnPlayClick);
        }

        private void OnBtnSettingClick()
        {
            //doozy stream
            Signal.Send(StreamId.FlowMainmenu.Setting);
        }

        private void OnBtnPlayClick()
        {
            //doozy stream
            Signal.Send(StreamId.FlowMainmenu.Ingame);
        }
    }
}