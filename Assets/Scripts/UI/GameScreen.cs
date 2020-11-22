using Events;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgeView> _carDodgeViewsList;

        public IEnumerator CarDodgeViewsCoroutine() {
            for (var i = 0; _carDodgeViewsList.Count > i; i++) {
                _carDodgeViewsList[i].Init();
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnEnable() {
            StartCoroutine(CarDodgeViewsCoroutine());
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}