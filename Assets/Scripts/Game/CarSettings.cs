using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName="CarSettings", menuName="Game/CarSettings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score settings")]
        [ValidateInput(nameof(ValidateNonNegative))]
        public int dodgeScore;

        [BoxGroup("Score settings")]
        [ValidateInput(nameof(ValidateNonNegative))]
        public float dodgeDistance;

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

        private bool ValidateNonNegative(double value) {
            return value >= 0;
        }


    }

}