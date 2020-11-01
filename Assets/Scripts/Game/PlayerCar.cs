using System.Collections;
using Events;
using UI;
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
        private ScriptableIntValue _score;

        private int _currentRoad;

        private bool _inDodge;

        public static bool CanAddScore = true;

        public static int DodgeScore;

        protected override void SubcribeToEvents() {
            base.SubcribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
        }

        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp(_currentRoad + _touchSide.value, -1, 1);
            var canDodge = !_inDodge && _currentSpeed >= _carSettings.maxSpeed && nextRoad != _currentRoad;
            if (!canDodge) {
                return;
            }
            StartCoroutine(DodgeCoroutine(nextRoad));
        }

        protected override void Move() {
            base.Move();
            _playerPositionZ.value = transform.position.z;
            AddScore();
        }

        private void AddScore() {
            if (EnemyCar.EnemyPositionZ != 0 && CanAddScore && _playerPositionZ.value > EnemyCar.EnemyPositionZ) {
                _score.value += DodgeScore;
                CanAddScore = false;
            }
        }

        protected override void OnCarCollision() {
            base.OnCarCollision();
            UIManager.Instance.ShowLeaderboardsScreen();
        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var offsetPerFrameX = _roadWidth.value / _dodgeDuration * (nextRoad > _currentRoad ? 1 : -1);
            while (timer < _dodgeDuration) {
                yield return null;
                timer += Time.deltaTime;
                transform.Translate(transform.right * offsetPerFrameX * Time.deltaTime);
            }

            _inDodge = false;
            _currentRoad = nextRoad;
        }

    }
}