using Events;
using System.Collections;
using UnityEngine;

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
        private ScriptableFloatValue _playerPositionX;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private Rewarder _dodgeRewarder;

        private int _currentRoad;
        private bool _inDodge;
        private bool _canReward = true;

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
            _playerPositionX.value = transform.position.x;
        }

        protected override void OnCarCollision() {
            base.OnCarCollision();
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
            if (_canReward) {
                _dodgeRewarder.SetScorePoints(_dodgedCar);
                StartCoroutine(RewardCoolDown());
            }
        }

        private IEnumerator RewardCoolDown() {
            Debug.Log("RewardCoolDown Coroutine started");
            _canReward = false;
            yield return new WaitForSeconds(1f);
            _canReward = true;
            Debug.Log("RewardCoolDown Coroutine ended");
        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var targetPosX = transform.position.x + _roadWidth.value * (nextRoad > _currentRoad ? 1 : -1);
            while(timer <= _dodgeDuration) {
                timer += Time.deltaTime;
                var posX = Mathf.Lerp(transform.position.x, targetPosX, timer/_dodgeDuration);
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                yield return null;
            }
            _inDodge = false;
            _currentRoad = nextRoad;
        }
    }
}
