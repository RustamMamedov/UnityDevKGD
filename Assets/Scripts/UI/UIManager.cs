using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace UI {
    public class UIManager : MonoBehaviour {

        public static UIManager instance;

        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;

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

        private void ShowMenuScreen() {
            _menuScreen.SetActive(true); 
        }

        private void ShowGameScreen() {
            _gameScreen.SetActive(true);
        }

        private void ShowLeaderboardScreen() {
            _leaderboardScreen.SetActive(true);
        }

        private void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
        }
    }
}

