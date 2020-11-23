using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is being increased by acceleration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [BoxGroup("Light")]
        [Range(1, 5)]
        public int carLightLenght;

        [BoxGroup("Render")]
        public GameObject renderCarPrefab;
        [BoxGroup("Render")]
        public Vector3 cameraPosition;
        [BoxGroup("Render")]
        public Quaternion cameraRotation;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}