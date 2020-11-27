using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;
namespace UI {

    public class Preloader : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingValue;

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");
            while (!asyncOperation.isDone) {
                _sceneLoadingValue.value = asyncOperation.progress/.9f;
                yield return null;
            }
        }
    }
}