using System;
using UnityEngine;

namespace Game {
    
    [CreateAssetMenu(fileName = "ScriptableStringValue", menuName = "ScriptableStringValue")]
    public class ScriptableStringValue : ScriptableObject {
    
        public string value = String.Empty;
    }
}