using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "Car/CarSettings")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed")]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [FoldoutGroup("Car light")]
        [Range(1, 5)]
        public float lightLength;

        [BoxGroup("Render settings")]
        public GameObject renderCarPrefab;

        [BoxGroup("Render settings")]
        public Quaternion _cameraRotation;

        [BoxGroup("Render settings")]
        public Vector3 _cameraPosition;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }

    }
}
