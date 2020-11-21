using System.Collections.Generic;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    public class RoadPart : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _roadTriggerEventDispatcher;

        [BoxGroup("Props positions")]
        [FoldoutGroup("Props positions/Rocks", false)]
        [SerializeField]
        private Transform _rockClusterPosition;

        [BoxGroup("Props positions")]
        [FoldoutGroup("Props positions/Signs", false)]
        [SerializeField]
        private Transform _streetSignPosition;

        [BoxGroup("Props positions")]
        [SerializeField]
        private List<Transform> _barrelPositions;

        [BoxGroup("Props positions")]
        [SerializeField]
        private List<Transform> _treesPositions;

        [BoxGroup("Props")]
        [SerializeField]
        private List<GameObject> _trees;

        [BoxGroup("Props")]
        [SerializeField]
        private List<GameObject> _rockClusters;

        [BoxGroup("Props")]
        [SerializeField]
        private List<GameObject> _barrels;

        [BoxGroup("Props")]
        [SerializeField]
        private List<GameObject> _streetSigns;

        [BoxGroup("Props")]
        [SerializeField]
        private GameObject _barrelWithWater;

        private Quaternion _layingBarrelRotation = Quaternion.Euler(0, -30, 90);

        private void OnEnable() {
            SetupRoad();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _roadTriggerEventDispatcher.Dispatch();
            }
        }

        private void SetupRoad() {
            SetupTrees();
            SetupRocks();
            SetupBarrels();
            SetupSigns();
        }

        private void SetupTrees() {
            foreach (Transform treePosition in _treesPositions) {
                var needToLocateTree = Random.Range(0, 2) > 0;
                if (needToLocateTree) {
                    Instantiate(GetRandomProp(_trees), treePosition);
                }
            }
        }

        private void SetupBarrels() {
            foreach (Transform barrelPosition in _barrelPositions) {
                var needToSpawnBarrel = Random.Range(0, 2) > 0;
                var barrelIsLaying = Random.Range(0, 10) > 8;
                var barrelHasWater = Random.Range(0, 10) > 7;

                if (!needToSpawnBarrel) {
                    continue;
                }

                if (barrelHasWater) {
                    Instantiate(_barrelWithWater, barrelPosition);
                    continue;
                }

                var barrel = Instantiate(GetRandomProp(_barrels), barrelPosition);
                if (barrelIsLaying) {
                    var positionWithOffset = new Vector3(barrel.transform.position.x, barrel.transform.position.y + 0.41f, barrel.transform.position.z);
                    barrel.transform.rotation = _layingBarrelRotation;
                    barrel.transform.position = positionWithOffset;
                }
            }

        }

        private void SetupSigns() {
            var needToSpawnSign = Random.Range(0, 2) > 0;
            if (needToSpawnSign) {
                Instantiate(GetRandomProp(_streetSigns), _streetSignPosition);
            }
        }

        private void SetupRocks() {
            var needToSpawnRocks = Random.Range(0, 2) > 0;
            if (needToSpawnRocks) {
                Instantiate(GetRandomProp(_rockClusters), _rockClusterPosition);
            }

        }

        private GameObject GetRandomProp(List<GameObject> listOfProps) {
            return listOfProps[Random.Range(0, listOfProps.Count)];
        }
    }
}
