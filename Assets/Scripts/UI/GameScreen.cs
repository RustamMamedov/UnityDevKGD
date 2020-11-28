using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace UI {
    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgeView> _carDodgeViewsList;

        public IEnumerator CarDodgedViewCoroutine() {
            for (int i = 0; i < _carDodgeViewsList.Count; i++) {
                _carDodgeViewsList[i].Init();
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnEnable() {
            StartCoroutine(CarDodgedViewCoroutine());
        }
        private void OnDisable() {
            RenderManager.Instance.ReleaseTexture();
        }
    }
}