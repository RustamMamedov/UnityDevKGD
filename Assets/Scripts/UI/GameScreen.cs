using UnityEngine;
using Events;
using System.Collections.Generic;
using System.Collections;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgeView> _carDodgeViewsList;

        [SerializeField]
        private Light _light;

        public IEnumerator CarDodgeViewsCoroutine() {
            _light.enabled = true;
            for (int i=0; i<_carDodgeViewsList.Count; i++) {
                _carDodgeViewsList[i].Init();
                yield return new WaitForEndOfFrame();
            }
            _light.enabled = false;
        }

        private void OnEnable() {
            StartCoroutine(CarDodgeViewsCoroutine());
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}