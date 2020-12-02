using System.Collections.Generic;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    public class Environment : MonoBehaviour {

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        [AssetsOnly]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _roadLength = 60;

        [SerializeField]
        private int _initialRoadNumber = 3;

        private List<Transform> _roadTransforms;

        private void Start() {
            GenerateRoad();
        }

        private void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollision;
        }

        private void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollision;
        }

        private void HandleRoadCollision() {
            MoveFirstRoadPart();
        }

        private void MoveFirstRoadPart() {
            var firstRoadPart = _roadTransforms[0];
            _roadTransforms.RemoveAt(0);
            firstRoadPart.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
            _roadTransforms.Add(firstRoadPart);
        }

        private void GenerateRoad() {
            _roadTransforms = new List<Transform>();
            for (int i = 0; i <= _initialRoadNumber; i++) {
                var position = new Vector3(0f, 0f, (i - 1) * _roadLength);
                var road = Instantiate(_roadPrefab, position, Quaternion.identity);
                _roadTransforms.Add(road.transform);
            }
        }
    }

}
