using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Events;
using Game;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgeView> _carDodgeViews = new List<CarDodgeView>();

        [SerializeField]
        private EventListener _carDodge;

        [SerializeField]
        private ScriptableIntValue _currentIdDodgeCar;

        private void Start() {
            _carDodge.OnEventHappened += HandleDodgeCar;
        }

        private void OnEnable() {
            StartCoroutine(CarDodgeCoroutine());
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();

        }

        private void HandleDodgeCar() {
            for (var numView = 0; numView < _carDodgeViews.Count; numView++) {
                _carDodgeViews[numView].CheckDodgeId(_currentIdDodgeCar);
            }
        }

        private IEnumerator CarDodgeCoroutine() {
            for (var numView = 0; numView < _carDodgeViews.Count; numView++) {
                _carDodgeViews[numView].Init();
                yield return new WaitForEndOfFrame();
            }
        }
    }
}