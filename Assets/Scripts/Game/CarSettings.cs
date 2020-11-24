using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

		public bool isEnemyCar;

        [ShowIf(nameof(isEnemyCar))]
        [FoldoutGroup("Type car", false)]
        public bool FamilyCar;
        [FoldoutGroup("Type car", false)]
        public bool SUV;
        [FoldoutGroup("Type car", false)]
        public bool Truck;

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is increased every frame", InfoMessageType.Info)]
        public float acceleration;

        [FoldoutGroup("Light settings", false)]
        [Range(1f, 100f)] 
        public float light;

        [ShowIf(nameof(isEnemyCar))]
        [FoldoutGroup("Score settings", false)]
        [ValidateInput(nameof(ValidateDodgeScore))]
   		public int dodgeScore;

        [ShowIf(nameof(isEnemyCar))]
        [FoldoutGroup("Render settings")]
        public GameObject renderCarPrefab;
        
        [ShowIf(nameof(isEnemyCar))]
        [FoldoutGroup("Render settings", false)]
        public Vector3 cameraPosition;

		[ShowIf(nameof(isEnemyCar))]
        [FoldoutGroup("Render settings", false)]
        public Quaternion cameraRotation;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}