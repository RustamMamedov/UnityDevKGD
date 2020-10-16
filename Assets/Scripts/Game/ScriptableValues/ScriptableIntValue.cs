using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "ScriptableIntValue", menuName = "ScriptableIntValue")]
    public class ScriptableIntValue : ScriptableObject {
        [SerializeField]
        public int value;
    }
}