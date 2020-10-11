using System;
using System.Collections;
using UnityEngine;

namespace UI {
    public class Fader : MonoBehaviour {

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private float _fadeTime;

        public event Action OnFadeIn = delegate { };
        public event Action OnFadeOut = delegate { };

        public void FedeIn() {
            StartCoroutine(FadeInCoroutine());
        }

        public void FedeOut() {
            StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeInCoroutine() {
            yield return StartCoroutine(FadeCoroutine(1f, 0f));
            OnFadeIn();
        }

        private IEnumerator FadeOutCoroutine() {
            yield return StartCoroutine(FadeCoroutine(0f, 1f));
            OnFadeOut();
        }

        private IEnumerator FadeCoroutine(float fromAlpha, float targetAlfa) {
            var timer = 0f;
            _canvasGroup.alpha = fromAlpha;

            while (timer < _fadeTime) {
                timer += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, targetAlfa, timer / _fadeTime);
                _canvasGroup.interactable = _canvasGroup.alpha > 0;
                yield return null;
            }
        }
    }
}