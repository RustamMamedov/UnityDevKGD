using Events;
using System.Collections;
using UnityEngine;
using Values;

namespace Game {
    
    public class PlayerCar : Car {

        // Fields.

        [SerializeField]
        private EventListener _touchEventListener;

        [SerializeField]
        private EventListener _daytimeChangedListener;

        [SerializeField]
        private ScriptableIntValue _touchSideValue;

        [SerializeField]
        private float _dodgeDuration;

        [SerializeField]
        private ScriptableFloatValue _laneWidthValue;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZValue;

        [SerializeField]
        private ScriptableIntValue _daytimeValue;

        [SerializeField]
        private GameObject[] _nightOnlyObjects;

        private int _currentRoad = 0;

        // Dodge coroutine is made to be called by Move() method (i.e. on Update custom event),
        // so dodge doesn't happen if Update is not invoked.
        // This is a reference for the current dodge coroutine.
        private IEnumerator _currentDodge = null;


        // Life cycle.

        private void Start() {
            UpdateNightOnlyObjects();
        }


        // Event handling.

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
            _daytimeChangedListener.OnEventHappened += UpdateNightOnlyObjects;
        }

        protected override void UnsubscribeFromEvents() {
            base.UnsubscribeFromEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
            _daytimeChangedListener.OnEventHappened -= UpdateNightOnlyObjects;
        }

        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp(_currentRoad + _touchSideValue.value, -1, 1);
            bool canDodge = _currentDodge == null && CurrentSpeed >= CarSettings.maxSpeed && nextRoad != _currentRoad;
            if (canDodge) {
                _currentDodge = MakeDodgeCoroutine(nextRoad);
            }
        }

        private void UpdateNightOnlyObjects() {
            foreach (var nightOnly in _nightOnlyObjects) {
                nightOnly.SetActive(_daytimeValue.value == 1);
            }
        }


        // Motion methods.

        protected override void Move() {
            base.Move();
            
            // Publish current z position.
            _playerPositionZValue.value = transform.position.z;

            // Call dodge coroutine. Set null when it's over.
            if (_currentDodge != null) {
                if (!_currentDodge.MoveNext()) {
                    _currentDodge = null;
                }
            }

        }

        private IEnumerator MakeDodgeCoroutine(int nextRoad) {
            float positionChangeRemained = nextRoad * _laneWidthValue.value - transform.position.x;
            float positionChangeSpeed = _laneWidthValue.value / _dodgeDuration;
            while (positionChangeRemained != 0) {
                float positionChange = Mathf.MoveTowards(0, positionChangeRemained, positionChangeSpeed * Time.deltaTime);
                transform.Translate(transform.right * positionChange);
                positionChangeRemained -= positionChange;
                yield return null;
            }
            _currentRoad = nextRoad;
        }


    }

}
