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
            asyncOperation.allowSceneActivation = false;
            while (asyncOperation.progress < 0.9f) {
                _screenFloatValue.value = asyncOperation.progress;
                yield return null;
            }
            _screenFloatValue.value = 1f;
            yield return new WaitForSeconds(2f);
            asyncOperation.allowSceneActivation = true;
        }
    }
}