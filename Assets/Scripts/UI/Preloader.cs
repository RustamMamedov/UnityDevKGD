using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UI {
    public class Preloader : MonoBehaviour {

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {//карутина - работает как поток из-за IEnumerator.
            var acyncOperation=SceneManager.LoadSceneAsync("Menu");
            while (!acyncOperation.isDone) {
                Debug.Log(acyncOperation.progress);
                yield return null;// yield -
                //если напишем new waitforfixedupdate - 
            }
        }
    }
}