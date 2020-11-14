using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

	[CreateAssetMenu(fileName = "Car Setting", menuName = "Game/Car Setting")]
	public class CarSettings : ScriptableObject {
	
        [FoldoutGroup("Speed", false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is increased every frame", InfoMessageType.Info)]
        public float acceleration;
		
        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
   		public int dodgeScore;
        [BoxGroup("Speed/Score")] 
        public int dodgeScore2;
        
        [FoldoutGroup("Gizmos", false)]
        [BoxGroup("Gizmos/LightSettings")] 
        [Range(1f, 5f)] 
        public float lightLenght;  
        

        private bool ValidateDodgeScore(int score) {
	        return score >= 0;
        }
	}
}