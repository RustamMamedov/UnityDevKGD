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

        [BoxGroup("Light")]
        [Range(1f, 60f)]
        public float lightLength;

        [BoxGroup("Render")]
        public GameObject renderCarPrefab;
        [BoxGroup("Render")]
        public Vector3 renderCameraPosition;
        [BoxGroup("Render")]
        public Vector3 renderCameraRotation;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}