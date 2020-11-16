using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "Car Assets", menuName = "Car/Settings")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [BoxGroup("Speed/Score")]
        public int dodgeScore2;

        [BoxGroup("Car Light")]
        [Range(1, 5)]
        public int carLight;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }

}
