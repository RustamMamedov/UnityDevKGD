using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "Cars", menuName = "Car/Cars")]
    public class Cars : ScriptableObject {

        public List<GameObject> carsList = new List<GameObject>();
    }
}
