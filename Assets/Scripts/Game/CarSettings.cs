using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]

        public int dodgeScore;
        [FoldoutGroup("Speed")]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is increased by acceleration every frame.", InfoMessageType.Warning)]
        public float acceleration;

        [Range(1, 5)]
        public float carLightLength;

        private bool ValidateDodgeScore(int score) {
            return (score > 0);
        }
    }
}