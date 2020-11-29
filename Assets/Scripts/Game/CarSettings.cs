using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Speed", false)] public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [BoxGroup("Speed/Score")] public int dodgeScore2;

        [FoldoutGroup("Lights Distance")]
        [Range(1, 60)]
        public static float lightDistance;

        [FoldoutGroup("UI Render")]
        public GameObject renderCarPrefab;

        [FoldoutGroup("UI Render/Camera Render")]
        public Vector3 cameraPosition;

        [FoldoutGroup("UI Render/Camera Render")]
        public Quaternion cameraRotation;

        public int differentCarCount;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}