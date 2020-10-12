using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace UI {
    public class UIManager : MonoBehaviour {

        public static UIManager instance;
        [SerializeField]
        private Fader _fader;
        private string _currentSceneName = "GamePlay";

        private void Awake() {
            if(instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnSceneFadeIn() {
            StartCoroutine(FadeOutAndLoadGamePlay());

        }

        private IEnumerator FadeOutAndLoadGamePlay() {
            yield return new WaitForSeconds(3f);
            _fader.OnFadeOut += LoadGamePlayScene;
            _fader.FadeOut();
        }

        private void LoadGamePlayScene() {
            _fader.OnFadeOut -= LoadGamePlayScene;
            StartCoroutine(LoadSceneCoroutine(_currentSceneName));
            _currentSceneName = _currentSceneName == "GamePlay" ? "Menu" : "GamePlay";
            
        }

        private IEnumerator LoadSceneCoroutine(string scenename) {
            var asyncOp = SceneManager.LoadSceneAsync(scenename);
            while (!asyncOp.isDone) {
                yield return null;
            }

            yield return new WaitForSeconds(3f);
            _fader.FadeIn();
        }


    }
}

