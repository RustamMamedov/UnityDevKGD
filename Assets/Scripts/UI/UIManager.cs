using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI {
    public class UIManager : MonoBehaviour {

        public static UIManager Instance;
        [SerializeField]
        private Fader _fader;
        
        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardScreen;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }

        public void LoadGamePlay() {
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();

        }
        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("GamePlay"));
            ShowGameScreen();
           
        }

        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen();

        }       

       private IEnumerator LoadSceneCoroutine(string sceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            _fader.FadeIn();
        }

        public void ShowMenuScreen() {
            HideAllScreen();
            _menuScreen.SetActive(true);
        }

        public void ShowGameScreen() {
            HideAllScreen();
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardScreen() {
            HideAllScreen();
            _leaderboardScreen.SetActive(true);
        }

        public void HideAllScreen() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardScreen.SetActive(false);
        }
    }
}