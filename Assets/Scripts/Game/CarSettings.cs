using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "New Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed")]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame")]
        public float acceleration;

        [FoldoutGroup("Distance")]
        [Range(1f, 5f)]
        public float lightDistance;

        [FoldoutGroup("RenderParameters")]
        public GameObject renderCarPrefab;
        [FoldoutGroup("RenderParameters")]
        public Vector3 renderCameraPosition;
        [FoldoutGroup("RenderParameters")]
        public Quaternion renderCameraRotation;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}

