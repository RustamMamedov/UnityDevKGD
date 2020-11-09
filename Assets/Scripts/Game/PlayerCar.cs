using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class PlayerCar : Car {

        [SerializeField]
        private EventListeners _touchEventListeners;

        [SerializeField]
        private EventDispatcher _saveDataEventDispatcher;

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

        protected override void OnCarCollision() {
            _saveDataEventDispatcher.Dispatch();
            base.OnCarCollision();
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
            var targetFromX = (transform.position.x + _roadWidth.Value * (nextRoad>_currentRoad?1:-1));
            while (timer < _dodgeDurtion) {
                timer += Time.deltaTime;
                var pos = Mathf.Lerp(transform.position.x, targetFromX, timer / _dodgeDurtion);
                transform.position = new Vector3(pos, transform.position.y, transform.position.z);
                yield return null;
            }

            _inDodge = false;
            _currentRoad = nextRoad;
        }

        protected override void Move() {
            base.Move();
            _positionPlayeZ.Value = transform.position.z;
        }

        //Gizmos для обозначения обьектов или каких либо диапазонов внутри редактора (информация для разработчика)

        private void OnDrawGizmos() {
            
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, 5f);
            Gizmos.DrawIcon(transform.position + Vector3.up * 4f, "");
            Gizmos.DrawFrustum(transform.position + transform.forward * 2, 15f, 10f, 50f, .5f);
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            Gizmos.DrawWireMesh(mesh,0,transform.position+transform.forward*5,Quaternion.identity,Vector3.one);
        }
    }
}
