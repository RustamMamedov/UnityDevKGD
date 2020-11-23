using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgeView> _carDodgeViews = new List<CarDodgeView>();

        private void OnEnable() {
            StartCoroutine(CarDodgeCoroutine());
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }

        private IEnumerator CarDodgeCoroutine() {

            for (var numView = 0; numView < _carDodgeViews.Count; numView++) {
                _carDodgeViews[numView].Init();
                yield return new WaitForEndOfFrame();
            }
        }
    }
}