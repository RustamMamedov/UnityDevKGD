using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using System;

namespace Game {
    public class PlayerCar : Car {
        [SerializeField]
        private EventListeners _touchEventListener;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        [SerializeField]
        private float _dodgeDuration;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableFloatValue _playerPisotionZ;

        private int _currentRoad;
        private bool _inDodge;
        protected override void SubscribeToEvent() {   
            base.SubscribeToEvent();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
        }
        protected override void UnsubscribeToEvent() {
            base.UnsubscribeToEvent();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
        }

        protected override void OnCarCollision() {
            base.OnCarCollision();
        }
        protected override void Move() {
            base.Move();
            _playerPisotionZ.value = transform.position.z;
        }
        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp(_currentRoad + _touchSide.value, -1, 1);
            var canDodge = !_inDodge && _currentSpeed >= _carSetting.maxSpeed && nextRoad != _currentRoad;
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
                var posX = Mathf.Lerp(transform.position.x, targetPosX,timer/_dodgeDuration);
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                yield return null;
            }

            _inDodge = false;
            _currentRoad = nextRoad;
        }
    }
}