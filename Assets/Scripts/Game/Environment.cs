using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class Environment : MonoBehaviour {

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _roadLength = 12;

        [SerializeField]
        private int _initialRoadNumber = 10;

        private List<Transform> _roadTransform;
    }

}
