using Game;
using System.Collections;
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

            var acyncOperation = SceneManager.LoadSceneAsync("Menu");
            acyncOperation.allowSceneActivation = false;
            while (acyncOperation.progress < 0.9f) {
                _sceneLoadingValue.value = acyncOperation.progress;
                yield return null;
            }
            _sceneLoadingValue.value = 1f;

            yield return new WaitForSeconds(2f);
            acyncOperation.allowSceneActivation = true;
        }
    }
}

