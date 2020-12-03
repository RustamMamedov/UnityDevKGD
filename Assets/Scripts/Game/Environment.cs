using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {
    public class Environment : MonoBehaviour {

        [SerializeField]
        private EventListeners _roadCollisionEventListener;

        [SerializeField]
        [AssetsOnly]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _initialRoadNumber = 10;

        [SerializeField]
        private int _roadLength = 12;

        [SerializeField]
        private Light _light;

        [SerializeField]
        private ScriptableIntValue _illumination;

        private List<Transform> _roadTransform;

        private void Start() {
            if(_illumination.value == 0) {
                _light.gameObject.SetActive(true);
            }
            else {
                _light.gameObject.SetActive(false);
            }
            GenerateRoad();
        }
        protected void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollision;
        }
        protected void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollision;
        }
        private void GenerateRoad() {
            _roadTransform = new List<Transform>();
            for (int i = 0; i < _initialRoadNumber + 1; i++) {
                var position = new Vector3(0f, 0f, (i - 1) * _roadLength);
                var road = Instantiate(_roadPrefab, position, Quaternion.identity);
                _roadTransform.Add(road.transform);
            }
        }
        private void HandleRoadCollision() {
            MoveFirstRoadPart();
        }
        private void MoveFirstRoadPart() {
            var firstRoadPart = _roadTransform[0];
            _roadTransform.RemoveAt(0);
            firstRoadPart.position = new Vector3(0f, 0f, _roadTransform[_roadTransform.Count - 1].position.z + _roadLength);
            _roadTransform.Add(firstRoadPart);
        }
    }
}