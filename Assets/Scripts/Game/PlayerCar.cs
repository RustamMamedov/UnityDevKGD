﻿using UnityEngine;
using Events;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;

namespace Game {
    public class PlayerCar : Car
    {
        [SerializeField]
        private EventListener _touchEventListener;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        [SerializeField]
        private float _dodgeDuration;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField] //для текущего места авто
        private ScriptableFloatValue _playerPositionZ;

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

        protected override void Move() {
            base.Move();
            _playerPositionZ.value = transform.position.z;
        }
        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp ( _currentRoad + _touchSide.value, -1, 1); //не уедем дальше -1 и 1
            var canDodge = !_inDodge && _currentSpeed >= _carSetting.maxSpeed && nextRoad != _currentRoad; // уворот только если игрок модет делать поворот, только  при мах скорости и есть дорога соединяющаяся с этой (след дорога равна текущей)
            if (!canDodge) {
                return;
            }
            StartCoroutine(DodgeCoroutine(nextRoad));
        }
        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var targetPosX = transform.position.x + _roadWidth.value * (nextRoad > _currentRoad ? 1 : -1);
            //var offsetPerFrameX = _roadWidth.value / _dodgeDuration * (nextRoad > _currentRoad ? 1 : -1) ; // ширина дороги на время поворота
            while (timer <= _dodgeDuration) {
                timer += Time.deltaTime;
                //transform.Translate(transform.right * offsetPerFrameX * Time.deltaTime);
                var posX = Mathf.Lerp(transform.position.x, targetPosX, timer / _dodgeDuration);
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                yield return null;
            }
            _inDodge = false;
            _currentRoad = nextRoad;
        }
    }
}

