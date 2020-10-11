using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace UI {
    public class UIManager : MonoBehaviour {

        public static UIManager Instance;

        [SerializeField]
        private Fader _fader;

        private string _CurrentSceneName = "Gameplay";

        private void Awake() {
            if (Instance) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _fader.OnFadeIn += OnSceneFadeIn;
            _fader.FedeIn();
        }

        private void OnSceneFadeIn() {
            StartCoroutine(FadeOutAndLoadGameplay());
        }

        private IEnumerator FadeOutAndLoadGameplay() {
            yield return new WaitForSeconds(1f);

            _fader.OnFadeOut += LoadGameplayScene;
            _fader.FedeOut();
        }

        private void LoadGameplayScene() {
            _fader.OnFadeOut-=LoadGameplayScene;
            StartCoroutine(LoadGameplaySceneCoroutine(_CurrentSceneName));
            _CurrentSceneName=_CurrentSceneName == "Gameplay" ? "Menu" : "Gameplay";
        }

        private IEnumerator LoadGameplaySceneCoroutine(string SceneName) {
            var asyncOperation = SceneManager.LoadSceneAsync(SceneName);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            _fader.FedeIn();
        }
    }
}