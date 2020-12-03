using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarAssets", menuName = "Car/Settings")]
    public class CarSettings : ScriptableObject {

        public enum CarType {
            Enemy,
            Player,
        }

        public enum EnemyType {
            SUV,
            Truck,
            Family,
        }

        public CarType carType;
        [ShowIf("carType", CarType.Enemy)]
        public EnemyType enemyType;

        [ValidateInput(nameof(ValidateDodgeScore))]
        [ShowIf("carType", CarType.Enemy)]
        public int dodgeScore;

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [Range(1f, 5f)]
        [ShowIf("carType", CarType.Player)]
        public float CarLight;

        [ShowIf("carType", CarType.Enemy)]
        [BoxGroup("Car Render")]
        public GameObject renderCarPrefab;

        [ShowIf("carType", CarType.Enemy)]
        [FoldoutGroup("Car Render/Camera Render", false)]
        public Vector3 position;

        [ShowIf("carType", CarType.Enemy)]
        [FoldoutGroup("Car Render/Camera Render", false)]
        public Quaternion rotation;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}