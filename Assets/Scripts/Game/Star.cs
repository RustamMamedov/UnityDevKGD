using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class Star : MonoBehaviour {

        private int _index;

        public int Index => _index;

        [SerializeField]
        private EventListener _starCollisionListener;

        [SerializeField]
        private ScriptableIntValue _starIndexValue;

        public void SetIndex(int index) {
            _index = index;
        }

        private void OnEnable() {
            _starCollisionListener.OnEventHappened += OnStarCollision;
        }

        private void OnDisable() {
            _starCollisionListener.OnEventHappened -= OnStarCollision;
        }

        private void OnStarCollision() {
            if (_index ==_starIndexValue.value) {
                gameObject.SetActive(false);
            }
        }



    }

}
