using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "Car setting", menuName = "Car setting")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Speed", false)]
        public int maxSpeed;
       
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is being increased by acceleration every frame", InfoMessageType.Info)]
        public float acceleration;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;
       
        [BoxGroup("Speed/Score")]
        public int dodgeScore2;

        [FoldoutGroup("Light", false)]
        [BoxGroup("Light/Length")]
        [Range(1f, 5f)] 
        public int lightLength;

        public GameObject renderCarPrefab;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}