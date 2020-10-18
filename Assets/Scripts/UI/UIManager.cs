using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance;
        [SerializeField]
        private Fader _fader;
        private string _currentSceneName = "Gameplay";

        

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private void Start() {
           
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
            StartCoroutine(LoadSceneCoroutine(_currentSceneName));
            _currentSceneName = _currentSceneName == "Gameplay" ? "Menu" : "Gameplay";
        }
        private IEnumerator LoadSceneCoroutine(string sceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            yield return new WaitForSeconds(3f);
            _fader.FadeIn();
        }

        public void ShowMenuScreen() {
            //показывает интерфейс онка меню
            
        }
        
        public void ShowGameScreen() {
            //показывает интерфейс окна игрового процесса
        }
        
        public void ShowLeaderboardsScreen() {
            //показывает таблицу лучших результатов
        }
        
        public void HideAllScreens() {
            //скрывает все экраны
        }
    }
}