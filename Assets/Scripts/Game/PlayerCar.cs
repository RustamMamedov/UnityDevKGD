using System.Collections;
using Audio;
using Events;
using UnityEngine;

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

        [SerializeField] 
        private ScriptableFloatValue _playerPositionX;
        
        [SerializeField] 
        private AudioSourcePlayer _carCollisionSourcePlayer;
        
        [SerializeField] 
        private AudioSourcePlayer _dodgeSourcePlayer;

        private int _currentRoad;
        private bool _inDodge;

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
        }

        protected override void UnsubscribeFromEvents() {
            base.UnsubscribeFromEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
        }
        
        protected override void OnCarCollision() {
            base.OnCarCollision();
            _carCollisionSourcePlayer.Play();
        }

        protected override void Move() {
            base.Move();
            _playerPositionZ.value = transform.position.z;
            _playerPositionX.value = transform.position.x;
        }

        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp(_currentRoad + _touchSide.value, -1, 1);
            var canDodge = !_inDodge && nextRoad != _currentRoad; //&& _currentSpeed >= _carSettings.maxSpeed 
            if (!canDodge) {
                return;
            }
            StartCoroutine(DodgeCoroutine(nextRoad));
        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var targetPosX = transform.position.x + _roadWidth.value * (nextRoad > _currentRoad ? 1 : -1);
            _dodgeSourcePlayer.Play();
            while (timer <= _dodgeDuration) {
                yield return null;
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