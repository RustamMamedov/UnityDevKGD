using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public bool isEnemyCar;

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        [ShowIf(nameof(isEnemyCar))]
        public int dodgeScore;

        [Range(1f, 5f)]
        public float lightDistance;

        [ShowIf(nameof(isEnemyCar))]
        [BoxGroup("Render")]
        public GameObject renderCarPrefab;

        [ShowIf(nameof(isEnemyCar))]
        [BoxGroup("Render/CameraSettings")]
        public Vector3 cameraPosition;

        [ShowIf(nameof(isEnemyCar))]
        [BoxGroup("Render/CameraSettings")]
        public Vector3 cameraRotation;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}