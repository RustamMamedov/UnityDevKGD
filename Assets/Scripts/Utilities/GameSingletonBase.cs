using UnityEngine;

namespace Utilities {
    
    public class GameSingletonBase<TSelf> : SceneSingletonBase<TSelf>
        where TSelf : GameSingletonBase<TSelf> {

        // Life cycle.

        protected override void Awake() {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }


    }

}
