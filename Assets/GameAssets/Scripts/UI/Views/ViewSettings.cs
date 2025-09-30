using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Input;
using UnityEngine;

namespace Gameplay.UI
{
    public class ViewSettings : MonoBehaviour
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
    }

}