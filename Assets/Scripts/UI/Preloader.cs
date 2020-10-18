using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;


namespace UI {
    public class Preloader : MonoBehaviour {

        [SerializeField]
        private ScriptableFloatValue _sceneLoadValue;

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {//карутина - работает как поток из-за IEnumerator.
            var acyncOperation=SceneManager.LoadSceneAsync("Menu");
            acyncOperation.allowSceneActivation = false;
            while (acyncOperation.progress<.9f) {
                _sceneLoadValue.Value = acyncOperation.progress;
                yield return null;// yield -
                //если напишем new waitforfixedupdate - 
            }
            _sceneLoadValue.Value = 1f;
            yield return new WaitForSeconds(2f);
            acyncOperation.allowSceneActivation = true;
        }
    }
}