using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName ="Car Settings",menuName = "Game/Car Settings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed",false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Increase speed every frame",InfoMessageType.Warning)]
        public float acceleration;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}

