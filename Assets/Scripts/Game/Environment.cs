using System.Collections.Generic;
using Events;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {
    
    public class Environment : MonoBehaviour {
        
#region Fields
[Header("Settings")]
        [SerializeField] 
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        [AssetsOnly]
        private GameObject _roadPrefab;

        [SerializeField] 
        private int _initialRoadNumber = 10;

        [SerializeField] 
        private int _roadLength = 12;

        private List<Transform> _roadTransforms;
        
#endregion

#region LifeCycle
        private void Start() {
            GenerateRoad();
        }

        private void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollision;
        }

        private void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollision;
        }
        
#endregion

#region Methods
        private void GenerateRoad() {
            _roadTransforms = new List<Transform>();
            for (int i = 0; i < _initialRoadNumber; ++i) {
                var pos = new Vector3(0f, 0f, (i - 1) * _roadLength);
                var road = Instantiate(_roadPrefab, pos, Quaternion.identity);
                _roadTransforms.Add(road.transform);
            }
        }
        
        private void HandleRoadCollision() {
            MoveFirstRoadPart();
        }

        private void MoveFirstRoadPart() {
            var firstRoadPart = _roadTransforms[0];
            _roadTransforms.RemoveAt(0);
            firstRoadPart.position = 
                new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
            _roadTransforms.Add(firstRoadPart);
        }
        
#endregion
        
    }
}


