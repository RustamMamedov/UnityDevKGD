using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class Preloader : MonoBehaviour {

        private void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("S_Menu");
            while (!asyncOperation.isDone) {
                Debug.Log(asyncOperation.progress);
                yield return null;
            }
        }
    }
}