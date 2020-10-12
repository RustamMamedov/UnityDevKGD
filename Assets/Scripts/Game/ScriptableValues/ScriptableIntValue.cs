using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "ScriptableInt", menuName = "ScriptableInt")]
    public class ScriptableIntValue : ScriptableObject {
        [SerializeField]
        public int value;
    }
}