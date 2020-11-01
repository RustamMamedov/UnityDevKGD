using System;
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
        private GameObject _leaderBoardScreen;

        private void Awake() {
            if (Instance) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadMenu() {
            
        }

        public void LoadGamePlay() {
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FedeOut();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut-=LoadGameplayScene;
            StartCoroutine(LoadGameplaySceneCoroutine("GamePlay"));
            ShowGameScreen();
        }

        public void LoadLeaderBoard() {
            ShowLeaderboardsScreen();
        }

        private IEnumerator LoadGameplaySceneCoroutine(string SceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(SceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            _fader.FedeIn();
        }
        public void ShowMenuScreen() {
            HideAllScreens();
            _menuScreen.SetActive(true);
        }

        public void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderBoardScreen.SetActive(true);
        }

        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderBoardScreen.SetActive(false);
        }
    }
}