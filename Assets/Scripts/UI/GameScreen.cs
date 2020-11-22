using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgedView> _listCarDodgedView;

        private void OnEnable() {
            CarDodgedViewInit();
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTexture();
        }

        private void CarDodgedViewInit() {
            StartCoroutine(CoroutineCarDodgedViewInit());
        }

        private IEnumerator CoroutineCarDodgedViewInit() {
            for (int i = 0; i < _listCarDodgedView.Count; i++) {
                _listCarDodgedView[i].OnInit();
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
