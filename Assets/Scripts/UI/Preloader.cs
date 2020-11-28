using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class Preloader : MonoBehaviour {

        [SerializeField]
        ScriptableFloatValue _sceneLoadingValue;

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");

            while (!asyncOperation.isDone) {
                _sceneLoadingValue.value = asyncOperation.progress / 0.9f;
                yield return null;
            }
        }
    }
}