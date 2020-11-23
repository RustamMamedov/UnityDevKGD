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

        [BoxGroup("Dodge")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;
        [BoxGroup("Dodge")]
        public int id;

        [BoxGroup("Lights")]
        [Range(1f, 5f)]
        public float lightDistance;

        [BoxGroup("Camera Render")]
        public Vector3 cameraPosition;
        [BoxGroup("Camera Render")]
        public Quaternion cameraRotation;
        [BoxGroup("Camera Render")]
        public GameObject renderCarPrefab;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}