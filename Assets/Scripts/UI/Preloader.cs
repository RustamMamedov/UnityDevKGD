using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;


namespace UI {

    
    public class Preloader : MonoBehaviour {

        [SerializeField] private ScriptableFloatValue _sceneLoading;
        private void Start() {
           StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");
            asyncOperation.allowSceneActivation = false;
            while (asyncOperation.progress < 0.9f) {
               _sceneLoading.value= asyncOperation.progress;
                yield return null;
            }
            _sceneLoading.value = 1f;

            yield return new WaitForSeconds(2f);
            asyncOperation.allowSceneActivation = true;
        }

    }
}