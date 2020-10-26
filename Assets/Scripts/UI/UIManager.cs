using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI {

    public class UIManager : MonoBehaviour {

        public static UIManager Instance;

        [SerializeField]
        private Fader _fader;

       // private string _currentSceneName = "Gameplay";

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
        public void ShowMenuScreen()
        {
            HideAllScreens();
            Debug.Log("ShowMenuScreen");
        }

        public void ShowGameScreen()
        {
            Debug.Log("ShowGameScreen");
        }

        public void ShowLeaderboardsScreen()
        {
            HideAllScreens();
            Debug.Log("ShowLeaderboardsScreen");
        }

        public void HideAllScreens()
        {
            Debug.Log("HideAllScreens");
        }
    }
}