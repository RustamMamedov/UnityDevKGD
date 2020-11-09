using Events;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private EventListener _gameSavedEventListener;

        private void Awake() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
            _gameSavedEventListener.OnEventHappened += ShowLeaderboard;
        }

        private void OnCarCollision() {

        }

        private void ShowLeaderboard() {
            UIManager.Instance.ShowLeaderboardsScreen();
        }
    }
}