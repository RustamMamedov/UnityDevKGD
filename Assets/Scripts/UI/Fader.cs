using System;
using System.Collections;
using UnityEngine;

namespace UI {
    
    public class Fader : MonoBehaviour {

        // Fields.

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private float _fadeTime;


        // Events.

        public event Action OnFadeIn = delegate {};
        public event Action OnFadeOut = delegate {};


        // Methods.

        public void FadeIn() {
            StartCoroutine(FadeInCoroutine());
        }

        public void FadeOut() {
            StartCoroutine(FadeOutCoroutine());
        }


        // Coroutines.

        private IEnumerator FadeInCoroutine() {
            yield return StartCoroutine(FadeCoroutine(0));
            OnFadeIn.Invoke();
        }

        private IEnumerator FadeOutCoroutine() {
            yield return StartCoroutine(FadeCoroutine(1));
            OnFadeOut.Invoke();
        }

        private IEnumerator FadeCoroutine(float targetedAlpha) {
            float alphaChangeSpeed = 1 / _fadeTime;
            while (_canvasGroup.alpha != targetedAlpha) {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, targetedAlpha, alphaChangeSpeed * Time.deltaTime);
                _canvasGroup.interactable = _canvasGroup.alpha > 0;
                yield return null;
            }
        }


    }

}