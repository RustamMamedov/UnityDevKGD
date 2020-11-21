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
		
        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        [ShowIf(nameof(isEnemyCar))]
   		public int dodgeScore;
        
        [FoldoutGroup("Gizmos", false)]
        [BoxGroup("Gizmos/LightSettings")] 
        [Range(1f, 5f)] 
        public float lightLenght;

        [ShowIf(nameof(isEnemyCar))]
        public GameObject renderCarPrefab;
        
        private bool ValidateDodgeScore(int score) {
	        return score >= 0;
        }
	}
}