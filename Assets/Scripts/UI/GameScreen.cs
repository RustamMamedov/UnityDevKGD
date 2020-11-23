using Events;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI {

    public class GameScreen : MonoBehaviour {
        
        [SerializeField]
        private List<CarDodgedView> _carsDodgedViews = new List<CarDodgedView>();

        private void OnEnable() {
            StartCoroutine(CarRendererCoroutine());
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }

        private IEnumerator CarRendererCoroutine() {
            foreach (var carsDodgedView in _carsDodgedViews) {
                carsDodgedView.Init();
                yield return null;
            }
        }
    }
}