using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Values;

namespace Loading {

    public class Preloader : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingProgress;

        private void Start() {
            StartCoroutine(LoadScene("Menu"));
        }

        private IEnumerator LoadScene(string sceneName) {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
            while (!loading.isDone) {
                _sceneLoadingProgress.value = loading.progress;
                yield return null;
            }
        }


    }

}