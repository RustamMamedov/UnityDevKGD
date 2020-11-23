using Events;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI {

    public class GameScreen : MonoBehaviour {
        
        [SerializeField]
        private List<CarDodgeVeiw> _carsDodgedViews = new List<CarDodgeVeiw>();

        private void OnEnable() {
            StartCoroutine(CarRendererCoroutine());
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTexture();
        }

        private IEnumerator CarRendererCoroutine() {
            foreach (var carsDodgedView in _carsDodgedViews) {
                carsDodgedView.Init();
                yield return new WaitForEndOfFrame();
            }
        }
    }
}