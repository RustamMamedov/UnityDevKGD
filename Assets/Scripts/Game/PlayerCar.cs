using UnityEngine;
using Events;
using System.Collections;

namespace Game {

    public class PlayerCar : Car {

        [SerializeField]
        private EventListener _touchEventListener;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        [SerializeField]
        private float _dodgeDuration;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        private int _currentRoad;

        private bool _inDodge;

        protected override void Move() {
            base.Move();
            _playerPositionZ.value = transform.position.z;
        }

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
        }

        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp( _currentRoad + _touchSide.value,-1,1);
            var canDodge = !_inDodge && _currentSpeed >= _carSettings.maxSpeed && nextRoad != _currentRoad;
            if (!canDodge) {
                return;
            }
            StartCoroutine(DodgeCoroutine(nextRoad));
        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var offsetPerFrameX = _roadWidth.value / _dodgeDuration * (nextRoad > _currentRoad ? 1 : -1);
            while (timer < _dodgeDuration) {
                yield return null;
                timer += Time.deltaTime;
                transform.Translate(transform.right * offsetPerFrameX * Time.deltaTime);
                Debug.Log(Time.deltaTime);
            }
            _inDodge = false;
            _currentRoad = nextRoad;
        }

    }
}
