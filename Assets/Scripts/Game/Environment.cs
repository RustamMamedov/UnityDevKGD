using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.Networking.Match;
using Sirenix.OdinInspector;

namespace Game {

    public class Environment : MonoBehaviour {
        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        [AssetsOnly]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _intialRoadNumber = 10;

        [SerializeField]
        private int _roadLenght = 12;

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
        private void GenerateRoad() {
            _roadTransforms = new List<Transform>();
            for (int i = 0; i < _intialRoadNumber+1; i++) {
                var position = new Vector3(0f, 0f, (i - 1) * _roadLenght);
                var road = Instantiate(_roadPrefab, position, Quaternion.identity);
                _roadTransforms.Add(road.transform);

            }
        }

        private void HandleRoadCollision() {
            MoveFirstRoadPart();
        }

        private void MoveFirstRoadPart() {
            var firstRoadPart = _roadTransforms[0];
            _roadTransforms.RemoveAt(0);
            firstRoadPart.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLenght);
            _roadTransforms.Add(firstRoadPart);

        }

    }
}
