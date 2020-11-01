using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "IntValue", menuName = "new IntValue")]
    public class ScriptableIntValue : ScriptableObject {
        public int value;
    }
}