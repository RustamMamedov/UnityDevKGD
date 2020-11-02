using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
namespace UI {
    public class EndGameShow : MonoBehaviour {

        [SerializeField]
        private EventListener _collisionEventListener;

        private void Awake() {
            _collisionEventListener.OnEventHappened += ShowLeaderboardScreen;
        }

        private void ShowLeaderboardScreen() {
            UIManager.Instance.ShowLeaderboardScreen();
        }

    }
}
