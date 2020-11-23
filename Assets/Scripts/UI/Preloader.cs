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
            while (!acyncOperation.isDone) {
                _sceneLoadingValue.value = acyncOperation.progress / 0.9f;
                yield return null;
            }
        }
    }
}

