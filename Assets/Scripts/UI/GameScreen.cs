using Events;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionEventListener;

        private void Awake() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnCarCollision() {
            UIManager.Instance.ShowLeaderboardScreen();
        }
    }
}