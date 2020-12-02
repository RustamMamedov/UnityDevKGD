using System.Collections;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class Preloader : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadValue;

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");
            while (asyncOperation.progress < 0.9f) {
                _sceneLoadValue.value = asyncOperation.progress / 0.9f;
                yield return null;
            }
        }
    }
}
