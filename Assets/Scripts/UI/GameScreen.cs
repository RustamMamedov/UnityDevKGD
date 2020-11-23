using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgeView> _carDodgedViews = new List<CarDodgeView>();

        private void OnEnable() {
            StartCoroutine(nameof(CarDodgeViewImagesSetup));
        }

        private IEnumerator CarDodgeViewImagesSetup() {
            foreach (CarDodgeView carDodgedView in _carDodgedViews) {
                carDodgedView.Init();
                yield return null;
            }
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}
