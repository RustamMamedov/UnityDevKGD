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

        [Range(1, 40)]
        public float carLightLength;

        [BoxGroup("UI Icon Utilities")]
        public GameObject renderCarPrefab;
        [BoxGroup("UI Icon Utilities")]
        public Vector3 cameraPosition;
        [BoxGroup("UI Icon Utilities")]
        public Quaternion cameraRotation;

        private bool ValidateDodgeScore(int score) {
            return (score > 0);
        }
    }
}