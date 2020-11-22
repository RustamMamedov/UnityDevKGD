using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "New String Value", menuName = "Data/StringValue")]
    public class ScriptableStringValue : ScriptableObject {

        public string value;
    }
}
