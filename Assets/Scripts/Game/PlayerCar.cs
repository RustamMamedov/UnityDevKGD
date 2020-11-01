using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class PlayerCar : Car {

        [SerializeField]
        private EventListeners _touchEventListeners;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        [SerializeField]
        private float _dodgeDurtion;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableFloatValue _positionPlayeZ;

        private int _currentRoad = 0;
        private bool _inDodge;

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _touchEventListeners.OnEventHappened += OnPlayerTouch;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _touchEventListeners.OnEventHappened -= OnPlayerTouch;
        }

        private void OnPlayerTouch() {
            var nextRoad =Mathf.Clamp( _currentRoad + _touchSide.Value,-1,1);
            var canDodge = !_inDodge && _currentSpeed >= _carSettings.maxSpeed && nextRoad != _currentRoad;
            if (!canDodge) {
                return;
            }
            StartCoroutine(DodgeCoroutine(nextRoad));
        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var offSetPerFrameX = (_roadWidth.Value / _dodgeDurtion *(nextRoad>_currentRoad?1:-1));
            while (timer < _dodgeDurtion) {
                yield return null;
                timer += Time.deltaTime;
                transform.Translate(transform.right * offSetPerFrameX * Time.deltaTime);
            }

            _inDodge = false;
            _currentRoad = nextRoad;
        }

        protected override void Move() {
            base.Move();
            _positionPlayeZ.Value = transform.position.z;
        }
    }
}
