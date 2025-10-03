using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Reactor.Animators;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using Gameplay;
using StansAssets.Foundation.Patterns;
using UnityEngine;

namespace Gameplay.UI
{
    public class ViewMainmenu : MonoBehaviour
    {
        public UIButton btnSetting;
        public UIButton btnPlay;
        public UIButton btnMerge;
        public UIButton btnDeal;
        private void Awake()
        {
            btnSetting.onClickEvent.AddListener(OnBtnSettingClick);
            btnPlay.onClickEvent.AddListener(OnBtnPlayClick);
            btnMerge.onClickEvent.AddListener(OnBtnMergeClick);
            btnDeal.onClickEvent.AddListener(OnBtnDealClick);
        }

       

        private void OnDestroy()
        {
            btnSetting.onClickEvent.RemoveListener(OnBtnSettingClick);
            btnPlay.onClickEvent.RemoveListener(OnBtnPlayClick);
            btnMerge.onClickEvent.RemoveListener(OnBtnMergeClick);
            btnDeal.onClickEvent.RemoveListener(OnBtnDealClick);
        }
        private void OnBtnMergeClick()
        {
            StaticBus<EventMerge>.Post(new());
        }

        private void OnBtnDealClick()
        {
            StaticBus<EventDeal>.Post(new());
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