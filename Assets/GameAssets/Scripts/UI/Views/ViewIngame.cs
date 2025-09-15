using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamepplay.UI
{
    public class ViewIngame : MonoBehaviour
    {
        public UIButton btnBack;
        private void Awake()
        {
            btnBack.onClickEvent.AddListener(OnBtnBackClick);
        }

        private void OnDestroy()
        {
            btnBack.onClickEvent.RemoveListener(OnBtnBackClick);
        }

        private void OnBtnBackClick()
        {
            BackButton.Fire();
        }

        private void Update()
        {
            // For testing purpose
            if (Input.GetKeyDown(KeyCode.W))
            {
                Signal.Send(StreamId.FlowMainmenu.Win);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Signal.Send(StreamId.FlowMainmenu.Lose);
            }
        }
    }
}
