using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {
    
    public class Preloader : MonoBehaviour {

        void Start() {
            StartCoroutine(LoadMenuScene());
        }

        private IEnumerator LoadMenuScene() {
            var asyncOperation = SceneManager.LoadSceneAsync("Menu");
            while (!asyncOperation.isDone) {
                Debug.Log(asyncOperation.progress);
                yield return null;
            }
        }
    }
}

