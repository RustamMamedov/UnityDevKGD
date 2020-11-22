using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class Environment : MonoBehaviour {
        [SerializeField]
        private EventListeners _roadCollisionsEventListeners;

        [SerializeField]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _initialRoadNumber;

        [SerializeField]
        private int _roadLength = 12;

        private List<Transform> _roadTransforms;

        private void Start() {
            GenerateRoad();
        }

        private void OnEnable() {
            _roadPrefab.SetActive(true);
            _roadCollisionsEventListeners.OnEventHappened += HandleRoadCollision;
        }

        private void OnDisable() {
            _roadCollisionsEventListeners.OnEventHappened -= HandleRoadCollision;
        }

        private void GenerateRoad() {
            _roadTransforms = new List<Transform>();
            for (int i=0;i<_initialRoadNumber+2;i++) {
                var position = new Vector3(0f, 0f, (i - 2) * _roadLength);
                var road = Instantiate(_roadPrefab, position, Quaternion.identity);
                _roadTransforms.Add(road.transform);
            }
            _roadPrefab.SetActive(false);
        }

        private void HandleRoadCollision() {
            MoveFirsRoadPart();
        }

        private void MoveFirsRoadPart() {
            var furstRoadPart = _roadTransforms[0];
            _roadTransforms.RemoveAt(0);
            furstRoadPart.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
            _roadTransforms.Add(furstRoadPart);
        }
    }
}
