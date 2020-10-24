using Events;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Environment : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        private GameObject _roadPartPrefab;

        [SerializeField]
        private int _initialRoadCount = 10;

        [SerializeField]
        private int _roadLength = 15;

        private List<Transform> _roadTransforms;


        // Life cycle.

        private void Start() {
            GenerateRoadParts();
        }

        private void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollision;
        }

        private void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollision;
        }


        // Event handling.

        private void HandleRoadCollision() {
            MoveFirstRoadPart();
        }
       

        // Road support methods.

        private void GenerateRoadParts() {
            _roadTransforms = new List<Transform>();
            for (int i = 0; i < _initialRoadCount; ++i) {
                var position = new Vector3(0f, 0f, (i - 1) * _roadLength);
                var road = Instantiate(_roadPartPrefab, position, Quaternion.identity, transform);
                _roadTransforms.Add(road.transform);
            }
        }

        private void MoveFirstRoadPart() {
            var firstRoadPart = _roadTransforms[0];
            _roadTransforms.RemoveAt(0);
            firstRoadPart.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
            _roadTransforms.Add(firstRoadPart);
        }


    }

}
