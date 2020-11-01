using Events;
using System.Collections;
using UnityEngine;
using UI;

namespace Game {
    public class PlayerCar : Car {


        [SerializeField]
        private EnemyCarToDodge _dodgedCar;

        [SerializeField]
        private EventListener _touchEventListener;

        [SerializeField]
        private EventListener _carDodgedListener;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        [SerializeField]
        private float _dodgeDuration;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private Rewarder _dodgeRewarder;

        private int _currentRoad;
        private bool _inDodge;

        protected override void SubScribeToEvents() {
            base.SubScribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
            _carDodgedListener.OnEventHappened += OnCarDodged;


        }

        protected override void UnSubScribeToEvents() {
            base.UnSubScribeToEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
            _carDodgedListener.OnEventHappened -= OnCarDodged;

        }

        protected override void Move() {
            base.Move();
            _playerPositionZ.value = transform.position.z;
            
        }

        protected override void OnCarCollision() {
            base.OnCarCollision();
            UIManager.instance.ShowLeaderboardScreen();
        }

        private void OnPlayerTouch() {
            
            var nextRoad = Mathf.Clamp(_currentRoad + _touchSide.value, -1, 1);
            var canDodge = !_inDodge && _currentSpeed >= _carSettings.maxSpeed && nextRoad != _currentRoad;
            if (!canDodge) {
                return;
            }
            
            StartCoroutine(DodgeCoroutine(nextRoad));

        }

        private void OnCarDodged() {
            _dodgeRewarder.SetScorePoints(_dodgedCar);
        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var offsetPerFrameX = _roadWidth.value / _dodgeDuration * (nextRoad > _currentRoad ? 1 : -1);
            while(timer < _dodgeDuration) {
                yield return null;
                timer += Time.deltaTime;
                transform.Translate(transform.right * offsetPerFrameX * Time.deltaTime);
            }

            _inDodge = false;
            _currentRoad = nextRoad;
        }
        
    }

}
