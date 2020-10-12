﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "ScriptableInt", menuName = "ScriptableInt")]
    public class ScriptableIntValue : ScriptableObject {
        public int value;
    }
}