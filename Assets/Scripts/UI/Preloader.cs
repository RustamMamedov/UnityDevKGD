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
            while (!acyncOperation.isDone) {
                _sceneLoadValue.Value = acyncOperation.progress/.9f;
                yield return null;// yield -
                //если напишем new waitforfixedupdate - 
            }
        }
    }
}