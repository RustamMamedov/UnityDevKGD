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
            while (asyncOperation.progress < 0.9f) {
                _sceneLoadingValue.value = asyncOperation.progress;
                yield return null;
            }
            _sceneLoadingValue.value = 1f;
            asyncOperation.allowSceneActivation = true;
        }
    }
}
