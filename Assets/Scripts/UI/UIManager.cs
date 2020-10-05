﻿using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace UI {
    public class UIManager : MonoBehaviour {

        public static UIManager Instance;

        [SerializeField]
        private Fader _fader;

        private string _currentSceneName = "GamePlay";

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _fader.OnFadeIn += OnSceneFadeIn;
            _fader.FadeIn();
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

    }
}
