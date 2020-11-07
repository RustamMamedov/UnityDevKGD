using System.Collections;
using System;
using UnityEngine;

namespace UI {

    public class Fader : MonoBehaviour {

        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float _fadeTime;

        public event Action OnFadeIn = delegate { };
        public event Action OnFadeOut = delegate { };

        public void FadeIn() {
            StartCoroutine(FadeInCoroutine());
        }

        public void FadeOut() {
            StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeInCoroutine() {
            yield return StartCoroutine(FadeCorouitne(1f, 0f));
            OnFadeIn();
        }

        private IEnumerator FadeOutCoroutine() {
            yield return StartCoroutine(FadeCorouitne(0f, 1f));
            OnFadeOut();
        }

        private IEnumerator FadeCorouitne(float fromAlpha, float targetAlpha) {
                var timer = 0f;
                _canvasGroup.alpha = fromAlpha;

                while (timer < _fadeTime) {
                    timer += Time.deltaTime;
                    _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, targetAlpha, timer / _fadeTime);
                    _canvasGroup.interactable = _canvasGroup.alpha > 0f;
                    yield return null;
                }
        }
    }
}

