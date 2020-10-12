using System;
using UnityEngine;

namespace Game {
    
    [CreateAssetMenu(fileName = "Score", menuName = "Score")]
    public class ScriptableIntValue : ScriptableObject {
        public int value;
    }
}
