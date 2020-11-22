using Events;
using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _gameSavedEventListener;

        private void Awake() {
            _gameSavedEventListener.OnEventHappened += ShowLeaderboard;
        }

        private void ShowLeaderboard() {
            UIManager.Instance.ShowLeaderboardsScreen();
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}