using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Info)]
        public float acceleration;

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [BoxGroup("Headlights")]
        [Range(1f, 5f)]
        public int headlightRange;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}
