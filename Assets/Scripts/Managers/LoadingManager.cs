using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Values;

namespace Managers {

    public class LoadingManager : MonoBehaviour {

        // Fields.

        [SerializeField]
        private ScriptableFloatValue _sceneLoadingProgress;


        // Life cycle.

        private void Start() {
            StartCoroutine(LoadScene("Menu"));
        }


        // Scene loading methods.

        private IEnumerator LoadScene(string sceneName) {
            AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
            while (!loading.isDone) {
                _sceneLoadingProgress.value = loading.progress / 0.9f;
                yield return null;
            }
        }


    }

}