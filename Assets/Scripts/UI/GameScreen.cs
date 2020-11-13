using Events;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionEventListener;

        private void Awake() {
            _carCollisionEventListener.OnEventHappened += ShowLeaderboardScreen;
        }

        private void ShowLeaderboardScreen() {
            UIManager.Instance.ShowLeaderboardScreen();
        }
    }
}