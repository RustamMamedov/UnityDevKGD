using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgedView> _carDodgedViews;

        private void OnEnable() {
            StartCoroutine(RenderCoroutine());
        }

        private void OnDisable() {
            RenderManager.Instanse.ReleaseTextures();
        }

        private IEnumerator RenderCoroutine() {
            for(int i = 0; i < _carDodgedViews.Count; i++) {
                _carDodgedViews[i].Init();
                yield return new WaitForEndOfFrame();
            }
        }

    }
}