﻿using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class Preloader : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingValue;

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone) {
                _sceneLoadingValue.value = asyncOperation.progress / .9f;
                yield return null;
            }


            // _sceneLoadingValue.value = 1f;

            // yield return new WaitForSeconds(2f);
            // asyncOperation.allowSceneActivation = true;
        }
    }
}