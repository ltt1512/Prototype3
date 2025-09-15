using Doozy.Runtime.Reactor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.UI
{
    public class ViewLoading : MonoBehaviour
    {
        public Progressor progressBar;

        private void Start()
        {
            progressBar.SetValueAt(0);
            StartCoroutine(LoadingCoroutine(1));
        }

        private IEnumerator LoadingCoroutine(int sceneId)
        {

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.SetValueAt(progress);
                yield return null;
            }

            // Loading complete, you can now switch to the next scene or perform other actions
            Debug.Log("Loading complete!");
        }
    }
}