using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [ShowInInspector] public List<BaseCtrl> baseCtrls { get; set; } = new();

        public AssetCtrl assetCtrl { get; set; }    
        public LevelCtrl levelCtrl { get; set; }
        public CoinCtrl coinCtrl { get; set; }
        public TubeCtrl tubeCtrl { get; set; }

        private void Awake()
        {
            AddListener();
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            Instance = this;
            baseCtrls = new List<BaseCtrl>(GetComponentsInChildren<BaseCtrl>());
            assetCtrl = baseCtrls.Find(ctrl => ctrl is AssetCtrl) as AssetCtrl;
            levelCtrl = baseCtrls.Find(ctrl => ctrl is LevelCtrl) as LevelCtrl; 
            coinCtrl = baseCtrls.Find(ctrl => ctrl is CoinCtrl) as CoinCtrl;
            tubeCtrl = baseCtrls.Find(ctrl => ctrl is TubeCtrl) as TubeCtrl;
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

        public static LevelCtrl GetLevelCtrl => Instance.levelCtrl;
        public static CoinCtrl GetCoinCtrl => Instance.coinCtrl;
        public static TubeCtrl GetTubeCtrl => Instance.tubeCtrl;
        public static AssetCtrl GetAssetCtrl => Instance.assetCtrl;
    }
}