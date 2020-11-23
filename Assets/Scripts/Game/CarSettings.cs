using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
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

        [FoldoutGroup("Light", false)]
        [BoxGroup("Light/Length")]
        [Range(1f, 5f)] 
        public int light;

        public GameObject renderCarPrefab;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}