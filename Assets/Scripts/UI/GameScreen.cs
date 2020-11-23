using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class GameScreen : MonoBehaviour {
        [SerializeField]
        private List<CarDodgeView> _carDodgeView;

        private void OnEnable() {
            StartCoroutine(CarDodgeViewInit());
        }

        private IEnumerator CarDodgeViewInit() {
            foreach(CarDodgeView carDodgeView in _carDodgeView) {
                carDodgeView.Init();
                yield return null;
            }
        }
        
        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}

