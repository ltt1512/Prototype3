using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Input;
using UnityEngine;
namespace Gamepplay.UI
{
    public class ViewLose : MonoBehaviour
    {
        public UIButton btnReplay;
        public UIButton btnHome;
        private void Awake()
        {
            btnReplay.onClickEvent.AddListener(OnBtnReplayClick);
            btnHome.onClickEvent.AddListener(OnBtnHomeClick);
        }

        private void OnDestroy()
        {
            btnReplay.onClickEvent.RemoveListener(OnBtnReplayClick);
            btnHome.onClickEvent.RemoveListener(OnBtnHomeClick);
        }

        private void OnBtnReplayClick()
        {
            BackButton.Fire();
        }

        private void OnBtnHomeClick()
        {
            Signal.Send(StreamId.FlowMainmenu.Ingame);
        }

    }
}
