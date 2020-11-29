using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class PlayerCar : Car {

        [SerializeField]
        private EventListener _touchEventListener;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        [SerializeField]
        private float _dodgeDuration;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        private int _currentRoad;
        private bool _inDodge;

        [SerializeField]
        private Light _lights1;

        [SerializeField]
        private Light _lights2;

        [SerializeField]
        private TrailRenderer _backLights1;

        [SerializeField]
        private TrailRenderer _backLights2;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableIntValue _timeState;


        private void Start() {
            _lights1.range = _carSettings.lightDistance;
            _lights2.range = _carSettings.lightDistance;

            switch (_timeState.value) {
                case 0:
                    _lights1.enabled = false;
                    _lights2.enabled = false;
                    _backLights1.enabled = false;
                    _backLights2.enabled = false;

                    break;
                case 1:
                    _lights1.enabled = true;
                    _lights2.enabled = true;
                    _backLights1.enabled = true;
                    _backLights2.enabled = true;

                    break;
            }
        }

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
        }

        protected override void Move() {
            base.Move();
            _playerPositionZ.value = transform.position.z;
        }


        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp(_currentRoad + _touchSide.value, -1, 1);
            var canDodge = !_inDodge && _currentSpeed >= _carSettings.maxSpeed && nextRoad != _currentRoad;
            if (!canDodge) {
                return;
            }
            StartCoroutine(DodgeCoroutine(nextRoad));

        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var targetPosX = transform.position.x + _roadWidth.value * (nextRoad > _currentRoad ? 1 : -1);

            while (timer <= _dodgeDuration) {
                timer += Time.deltaTime;
                var posX = Mathf.Lerp(transform.position.x, targetPosX, timer / _dodgeDuration);
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                yield return null;
            }

            _inDodge = false;
            _currentRoad = nextRoad;
        }
    }

}
