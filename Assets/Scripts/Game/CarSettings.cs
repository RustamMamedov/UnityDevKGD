using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    
    public class CarSettings : ScriptableObject {

        public enum CarType {
            Enemy,
            Player,
        }

        public enum EnemyType {
            SUV,
            Truck,
            FamilyCar,
        }

        public CarType carType;

        [ShowIf("carType", CarType.Enemy)]
        public EnemyType enemyType;

        [ShowIf("carType", CarType.Enemy)]
        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed")]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is increased by acceleration every frame.", InfoMessageType.Warning)]
        public float acceleration;

        [ShowIf("carType", CarType.Player)]
        [Range(1, 40)]
        public float carLightLength;

        [ShowIf("carType", CarType.Enemy)]
        [BoxGroup("UI Icon Utilities")]
        public GameObject renderCarPrefab;

        [ShowIf("carType", CarType.Enemy)]
        [BoxGroup("UI Icon Utilities")]
        public Vector3 cameraPosition;
        
        [ShowIf("carType", CarType.Enemy)]
        [BoxGroup("UI Icon Utilities")]
        public Quaternion cameraRotation;

        private bool ValidateDodgeScore(int score) {
            return (score > 0);
        }
    }
}