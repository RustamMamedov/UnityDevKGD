using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "New Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {
        
        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;
        [BoxGroup("Score")]

        public int dodgeScore2;
        [FoldoutGroup("Speed")]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by this value every frame")]
        public float acceleration;

        [BoxGroup("LightSettings")]
        [Range(1f, 5f)]
        public int lightDistance;

        public GameObject renderCarPrefab;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}

