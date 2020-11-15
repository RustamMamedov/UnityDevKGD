using UnityEngine;
using Sirenix.OdinInspector;
namespace Game {

    [CreateAssetMenu(fileName = "NewCar", menuName ="Cars")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;
        [BoxGroup("Score")]
        public int dodgeScore2;
        [FoldoutGroup("Speed")]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame")]
        public float acceleration;
        [FoldoutGroup("Distance")]
        [ValidateInput(nameof(ValidateLightDistance))]
        public float lightDistance;
        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }

        private bool ValidateLightDistance(float dist) {
            return (dist >= 1f && dist <= 5f);
        }
    }

}
