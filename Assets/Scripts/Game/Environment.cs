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
    }
}
