using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Speed",false)]
        public int maxSpeed;
        [FoldoutGroup("Speed")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int acceleration;

        public int dodgeScore;

        [Range(1,5)]
        public int lightDistance;

        public GameObject renderCarPrefab;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }

    }
}