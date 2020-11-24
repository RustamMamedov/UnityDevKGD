using Events;
using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private List<CarSettings> _carSettings = new List<CarSettings>();

        [SerializeField]
        private List<CarDodgeView> _carsDodgedViews = new List<CarDodgeView>();

        [SerializeField]
        private EventListener _carDodged;

        [SerializeField]
        private ScriptableIntValue _dodgeCarId;

        private void Start() {
            _carDodged.OnEventHappened += CheckId;
        }

        private void OnEnable() {
            StartCoroutine(CarRendererCoroutine());
        }

        private void Awake() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnCarCollision() {
            UIManager.Instance.ShowLeaderboardScreen();
            foreach (var carDodgedView in _carsDodgedViews) {
                carDodgedView.ClearScoreLabel();
            }
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
        private IEnumerator CarRendererCoroutine() {
            for (int i = 0; i < _carsDodgedViews.Count; i++) {
                _carsDodgedViews[i].Init(_carSettings[i].renderCarPrefab, _carSettings[i].cameraPosition, _carSettings[i].cameraRotation);
                yield return new WaitForEndOfFrame();
            }
        }
        private void CheckId() {
            for (var i = 0; i < _carsDodgedViews.Count; i++) {
                _carsDodgedViews[i].CheckDodgeId(_dodgeCarId);
            }
        }
    }
}