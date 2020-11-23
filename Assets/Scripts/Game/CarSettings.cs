using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName="CarSettings", menuName="Game/CarSettings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score settings")]
        [ValidateInput(nameof(ValidateScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed settings")]
        public float maxSpeed;

        [FoldoutGroup("Speed settings", false)]
        public float acceleration;

        [BoxGroup("Icon rendering settings")]
        public GameObject renderableCarPrefab;

        [BoxGroup("Icon rendering settings")]
        [PropertyTooltip("The distance between rendered object and render camera.")]
        public float renderDistance;

        [BoxGroup("Other settings")]
        [PropertyRange(1f, 5f)]
        public float carLightDistance;

        private bool ValidateScore(int score) {
            return score >= 0;
        }


    }

}