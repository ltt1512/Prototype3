using Sirenix.OdinInspector;
using StansAssets.Foundation.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [ShowInInspector] public List<BaseCtrl> baseCtrls { get; set; }

        private void Awake()
        {
            AddListener();
            Application.targetFrameRate = 60;
            Instance = this;
            baseCtrls = new List<BaseCtrl>(GetComponentsInChildren<BaseCtrl>());
            
            Init();
        }

        private void OnDestroy()
        {
            RemoveListener();
            Instance = null;
        }

        private void AddListener()
        {
            
        }


        private void RemoveListener()
        {
           
        }

        


        public void Init()
        {
            foreach (var ctrl in baseCtrls)
            {
                ctrl.Init();
            }
        }

        void Update()
        {
        }

       

        public void StartGame()
        {
            foreach (var ctrl in baseCtrls)
            {
                ctrl.StartGame();
            }
        }

        public void Reset()
        {
            foreach (var ctrl in baseCtrls)
            {
                ctrl.Reset();
            }
        }

       

    }
}