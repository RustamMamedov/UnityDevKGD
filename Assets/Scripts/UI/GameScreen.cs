using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarSettings> _carSettings = new List<CarSettings>();
        [SerializeField]
        private List<CarDodgedView> _carsDodgedViews = new List<CarDodgedView>();

        private bool _isCoolDownEnded = true;

        private void OnEnable() {
            StartCoroutine(InitOfCarDodgeView());
        }
        private void OnDisable() {
            RenderManager.Instance.ReleaseTexture();
        }

        private void DodgeCounter() {
            if (_isCoolDownEnded) {
                for (int i = 0; i < _carSettings.Count;i++) {
                    
                }
            }
        }

        IEnumerator InitOfCarDodgeView() {
            for (int i = 0; i < _carsDodgedViews.Count; i++) {
                _carsDodgedViews[i].Init(_carSettings[i].renderCarPrefab,_carSettings[i].renderCameraPosition);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
