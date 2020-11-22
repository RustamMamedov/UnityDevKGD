using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings", order = 0)]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Info", false)] 
        public string carName;
        
        [FoldoutGroup("Speed", false)]
        public float acceleration;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float maxSpeed;
        
        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore = 0;

        [FoldoutGroup("Lights", false)] 
        [Range(1f, 5f)] 
        public float lightDistance;
        
        [FoldoutGroup("UI Render", false)]
        public GameObject renderCarPrefab;

        [FoldoutGroup("UI Render/Camera Render", false)] 
        public Vector3 cameraPosition;
        
        [FoldoutGroup("UI Render/Camera Render", false)] 
        public Quaternion cameraRotation;
        
        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
        
    }
}
