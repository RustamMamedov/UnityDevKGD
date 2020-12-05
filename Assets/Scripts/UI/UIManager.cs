using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;
using UI;
using Game;
using Sirenix.OdinInspector;
using UnityEngine.UI;

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

        [SerializeField]
        private GameObject _settingsScreen;

        [SerializeField]
        private MusicManager _musicManager;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue[] _curr;

        [SerializeField]
        private ScriptableIntValue[] _currentScoreCar;

        [SerializeField]
        private Text[] _scoreText;

        [SerializeField]
        private Text _score;

        [SerializeField]
        private ScriptableFloatValue _music;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            ShowMenuScreen();
        }

        public void LoadMenu() {
            _fader.OnFadeOut += LoadMenuScene;
            _fader.FadeOut();
        }

        public void LoadGameplay() {
            _curr[0].value = 0;
            _curr[1].value = 0;
            _curr[2].value = 0;
            _curr[3].value = 0;
            _currentScore.value = 0;
            _score.text = "0";
            _currentScoreCar[0].value = 0;
            _currentScoreCar[1].value = 0;
            _currentScoreCar[2].value = 0;
            _scoreText[0].text = "0";
            _scoreText[1].text = "0";
            _scoreText[2].text = "0";
            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadMenuScene() {
            _fader.OnFadeOut -= LoadMenuScene;
            StartCoroutine(LoadSceneCoroutine("Menu"));
            ShowMenuScreen();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            StartCoroutine(LoadSceneCoroutine("Gameplay"));
            ShowGameScreen();
        }

        private IEnumerator LoadSceneCoroutine(string sceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }

            _fader.FadeIn();
        }

        public void ShowMenuScreen() {
                HideAllScreens();
                _menuScreen.SetActive(true);
                _musicManager.PlayMenuMusic();
        }

        public void ShowGameScreen() {
            HideAllScreens();
            _gameScreen.SetActive(true);
            _musicManager.PlayGameMusic();
        }

        public void ShowLeaderboardsScreen() {
            HideAllScreens();
            _leaderboardScreen.SetActive(true);
        }

        public void ShowSettingsScreen() {
            HideAllScreens();
            _settingsScreen.SetActive(true);
            _musicManager.PlayMenuMusic();
        }

        public void HideAllScreens() {

            if (_menuScreen.activeSelf == true) {
                _musicManager.StopMenuMusic();
                _menuScreen.SetActive(false);
            }
            if (_gameScreen.activeSelf == true) {
               _musicManager.StopGameMusic();
                _gameScreen.SetActive(false);
            }
            if (_leaderboardScreen.activeSelf == true) {
                _leaderboardScreen.SetActive(false);
            }
            if (_settingsScreen.activeSelf == true) {
                _musicManager.StopMenuMusic();
                _settingsScreen.SetActive(false);
            }
        }

    }
}

