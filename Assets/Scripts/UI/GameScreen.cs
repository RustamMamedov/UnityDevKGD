using Events;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _resultsSavedEventListener;

        private void Awake() {
            _resultsSavedEventListener.OnEventHappened += OnCarCollision;
        }

        private void OnCarCollision() {
            UIManager.Instance.ShowLeaderboardScreen();
        }
    }
}