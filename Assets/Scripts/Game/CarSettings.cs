using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName ="Car Settings",menuName = "Game/Car Settings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [BoxGroup("Car Light")]
        [Range(1, 5)]
        public float lightRange;

        [FoldoutGroup("Speed",false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Increase speed every frame",InfoMessageType.Warning)]
        public float acceleration;

        [BoxGroup("Render Settings")]
        public GameObject renderCarPrefab;
        [BoxGroup("Render Settings")]
        public Vector3 renderCameraPosition;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}

