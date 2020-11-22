using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public enum CarType {
            Enemy,
            Player,
        }

        public CarType carType;

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Info)]
        public float acceleration;

        [ValidateInput(nameof(ValidateDodgeScore))]
        [ShowIf("carType", CarType.Enemy)]
        public int dodgeScore;

        [ShowIf("carType", CarType.Enemy)]
        public GameObject renderCarPrefab;

        [Range(1f, 5f)]
        [ShowIf("carType", CarType.Player)]
        public int headlightRange;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}
