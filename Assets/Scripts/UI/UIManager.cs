using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

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
        private GameObject _leaderboardsScreen;
        
        private string _currentSceneName = "GamePlay";

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnSceneFadeIn() {
            StartCoroutine(FadeOutAndLoadGameplay());
        }

        private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(3f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FadeOut();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut -= LoadGameplayScene;
            SceneManager.LoadScene("GamePlay");
            _currentSceneName = _currentSceneName == "GamePlay" ? "Menu" : "GamePlay";
        }

        private IEnumerator LoadGameplaySceneCoroutine(string sceneName) {
            var asincOperation  = SceneManager.LoadSceneAsync(sceneName);
            while (!asincOperation.isDone) {
                yield return null;
            }

            yield return new WaitForSeconds(3f);
            _fader.FadeIn();
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
            _leaderboardsScreen.SetActive(true);
        }
        
        public void HideAllScreens() {
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardsScreen.SetActive(false);
        }
    }
}
