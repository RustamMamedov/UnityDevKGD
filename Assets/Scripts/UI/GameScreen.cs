using UnityEngine;
using Events;
using System.Collections.Generic;
using System.Collections;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgeView> _carDodgeViewsList;

        public IEnumerator CarDodgeViewsCoroutine() {
            for (int i=0; i<_carDodgeViewsList.Count; i++) {
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
