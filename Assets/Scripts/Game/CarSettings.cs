using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

	[CreateAssetMenu(fileName = "Car Setting", menuName = "Game/Car Setting")]
	public class CarSettings : ScriptableObject {

		public bool isEnemyCar;
		
        [FoldoutGroup("Speed", false)]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is increased every frame", InfoMessageType.Info)]
        public float acceleration;

        [FoldoutGroup("Car light settings", false)]
        [Range(1f, 5f)] 
        public float lightLenght;

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