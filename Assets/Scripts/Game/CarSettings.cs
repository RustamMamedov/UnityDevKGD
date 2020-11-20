using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public bool isEnemyCar;

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        [ShowIf(nameof(isEnemyCar))]
        public int dodgeScore;

        [FoldoutGroup("Speed")]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        public float acceleration;

        [Range(1f, 5f)]
        public float lightLength;

        [ShowIf(nameof(isEnemyCar))]
        public GameObject renderCarPrefab;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}