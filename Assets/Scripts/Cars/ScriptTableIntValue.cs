using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "Score", menuName = "new Score")]
    public class ScriptTableIntValue : ScriptableObject {
        [SerializeField]
        public int Score;
    }
}