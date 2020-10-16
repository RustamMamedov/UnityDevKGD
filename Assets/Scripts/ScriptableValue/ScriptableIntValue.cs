using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "Score", menuName = "new Score")]
    public class ScriptableIntValue : ScriptableObject {

        public int Value;
    }
}
