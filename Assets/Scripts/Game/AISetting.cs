using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "AISettings", menuName = "AISettings")]
    public class AISettings : ScriptableObject {

        [InfoBox("Distance should be positive even value", InfoMessageType.Error)]
        public int value;
    }
}
