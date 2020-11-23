using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "Car setting", menuName = "Car setting")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Speed", false)]
        public int maxSpeed;
       
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is being increased by acceleration every frame", InfoMessageType.Info)]
        public float acceleration;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;
       
        [BoxGroup("Speed/Score")]
        public int dodgeScore2;

        [FoldoutGroup("Light", false)]
        [BoxGroup("Light/Length")]
        [Range(1f, 100f)] 
        public int lightLength;

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