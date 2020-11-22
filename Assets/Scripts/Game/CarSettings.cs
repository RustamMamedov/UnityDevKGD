using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings", order = 0)]
    public class CarSettings : ScriptableObject {
        
        [FoldoutGroup("Speed", false)]
        public float acceleration;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float maxSpeed;
        
        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore = 0;

        [BoxGroup("Lights")] 
        [Range(1f, 5f)] 
        public float lightDistance;

        public GameObject renderCarPrefab;
        
        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
        
    }
}
