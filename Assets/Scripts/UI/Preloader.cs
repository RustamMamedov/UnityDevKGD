using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;
namespace UI {

    public class Preloader : MonoBehaviour {
        [SerializeField]
        private ScriptableFloatValue _screenFloatValue;

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");
            while (!asyncOperation.isDone) {
                _screenFloatValue.value = asyncOperation.progress / .9f;
                yield return null;
            }
        }
    }
}