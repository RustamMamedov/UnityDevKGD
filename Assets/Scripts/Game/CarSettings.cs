using UnityEngine;

namespace Game {

	[CreateAssetMenu(fileName = "Car Setting", menuName = "Game/Car Setting")]
	public class CarSettings : ScriptableObject {
	
   		public int dodgeScore;
        public float maxSpeed;
        public float acceleration;

	}
}