using Events;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _saveEventListener;

        private void Awake() {
            _saveEventListener.OnEventHappened += OnGameSaved;
        }

        private void OnGameSaved() {
            UIManager.Instance.ShowLeaderboardScreen();
        }
    }
}