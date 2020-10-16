using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "Score", menuName = "Score")]
    public class ScriptableIntValue : ScriptableObject {
        public int value;
    }
}