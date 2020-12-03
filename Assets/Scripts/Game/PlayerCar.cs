using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Events;
using UI;
using Audio;

namespace Game {

    public class PlayerCar : Car {

        [SerializeField]
        private Light _pointLight;

        [SerializeField]
        private CarSettings _settings;

        [SerializeField]
        private ScriptableIntValue _timeMode;

        [SerializeField]
        private GameObject _light;

        [SerializeField]
        private List<GameObject> _trails;

        [SerializeField]
        private ScriptableIntValue _currentScore;

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
        private Color _gizmosColor = Color.white;

        [SerializeField]
        private AudioSourcePlayer _dodgePlayer;

        private int _currentRoad;
        private bool _inDodge;
        private float _pointRange;
        private float _spotRange;

        private void Awake() {
            if (_timeMode.value == 1) {
                Debug.Log("log");
                _pointRange = _pointLight.range;
                _spotRange = _settings.lightLength;
                _pointLight.range=0;
                _settings.lightLength = 0;
                for(int i = 0; i < _trails.Count; i++) {
                    _trails[i].SetActive(false);
                }
                _light.SetActive(true);
            }
            else {
                _pointLight.range = _pointRange;
                _settings.lightLength = _spotRange;
                for (int i = 0; i < _trails.Count; i++) {
                    _trails[i].SetActive(true);
                }
                _light.SetActive(false);
            }
        }

        protected override void SubscribeToEvents() {
            _currentScore.value = 0;
            base.SubscribeToEvents();
            _touchEventListener.OnEventHappened += OnPlayerTouch;
        }
        protected override void UnsubscribeToEvents() {
            base.UnsubscribeToEvents();
            _touchEventListener.OnEventHappened -= OnPlayerTouch;

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
            StartCoroutine(DodgeCoroutine(nextRoad));
        }
        private IEnumerator DodgeCoroutine(int nextRoad) {
            _inDodge = true;
            var timer = 0f;
            _dodgePlayer.Play();
            var targetPosX = transform.position.x + _roadWidth.value * (nextRoad > _currentRoad ? 1 : -1);
            while (timer < _dodgeDuration) {
                timer += Time.deltaTime;
                var posX = Mathf.Lerp(transform.position.x, targetPosX, timer / _dodgeDuration);
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                yield return null;
            }
            _inDodge = false;
            _currentRoad = nextRoad;
        }
        private void OnDrawGizmosSelected() {
            /*Gizmos.color = _gizmosColor;

            Gizmos.DrawWireSphere(transform.position, 5f);
            Gizmos.DrawIcon(transform.position + Vector3.up * 4f, "car_gizmo");
            Gizmos.DrawFrustum(transform.position + transform.forward * 2, 45f, 15f, 50f, .5f);
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            Gizmos.DrawWireMesh(mesh, 0, transform.position + transform.forward * 5);*/

        }
    }
}
