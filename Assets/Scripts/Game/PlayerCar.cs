using UnityEngine;
using System.Collections.Generic;
using Events;
using System.Collections;

namespace Game {
    
    public class PlayerCar : Car {

        [SerializeField]
        private EventListener _touchEventListener;

        [SerializeField]
        private EventListener _dodgeEventListener;

        [SerializeField]
        private ScriptableIntValue _touchSide;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private float _dodgeDuration;

        [SerializeField]
        private AudioSource _moveSound;

        [SerializeField]
        private AudioSource _dodgeSound;

        [SerializeField]
        private AudioSource _collideSound;

        [SerializeField]
        private List<Light> _carLights;

        [SerializeField]
        private List<GameObject> _lightTrails;

        private int _currentRoad;
        private bool _inDodge;

        protected override void SubscribeToEvents() {
            base.SubscribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
            _dodgeEventListener.OnEventHappened += OnPlayerDodge;
        }

        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;
            _dodgeEventListener.OnEventHappened -= OnPlayerDodge;
        }

        protected override void OnCarCollision() {
            _collideSound.Play();
            UnsubscribeToEvents();
        }

        protected override void Move() {
            base.Move();
            _playerPositionZ.value = transform.position.z;
        }

        private void OnPlayerTouch() {
            var nextRoad = Mathf.Clamp(_currentRoad + _touchSide.value, -1, 1);
            var canDodge = !_inDodge && _currentSpeed >= _carSettings.maxSpeed && nextRoad != _currentRoad;

            if (!canDodge) {
                return;
            }

            _moveSound.Play();

            StartCoroutine(DodgeCoroutine(nextRoad));
        }

        private void OnPlayerDodge() {
            _dodgeSound.Play();
        }

        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            var targetPosX = transform.position.x + _roadWidth.value * (nextRoad > _currentRoad ? 1 : -1);

            while (timer <= _dodgeDuration) {
                yield return null;
                timer += Time.deltaTime;
                var posX = Mathf.Lerp(transform.position.x, targetPosX, timer / _dodgeDuration);
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            }

            _inDodge = false;
            _currentRoad = nextRoad;
        }
        
        private void OnDrawGizmosSelected() {
            Gizmos.color = _gizmosColor;

            Gizmos.DrawWireSphere(transform.position, 5f);
            Gizmos.DrawIcon(transform.position + Vector3.up * 4f, "car_gizmo");
            Gizmos.DrawFrustum(transform.position + transform.forward * 2, 45f, 15f, 50f, .5f);
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            Gizmos.DrawWireMesh(mesh, 0, transform.position + transform.forward * 5, Quaternion.identity);
        }

        public void SetLightingState(bool newState) {
            for (int i = 0; i < _carLights.Count; i++) {
                _carLights[i].enabled = newState;
            }

            for (int i = 0; i < _lightTrails.Count; i++) {
                _lightTrails[i].SetActive(newState);
            }
        }
    }
}