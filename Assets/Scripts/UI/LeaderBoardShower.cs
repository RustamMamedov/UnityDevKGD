using UnityEngine;
using Events;

namespace UI {

    public class LeaderBoardShower : MonoBehaviour {

        [SerializeField]
        private EventListener _gameSavedListener;

        private void OnEnable() {
            _gameSavedListener.OnEventHappened += ShowLeaderBoard;
        }

        private void OnDisable() {
            _gameSavedListener.OnEventHappened -= ShowLeaderBoard;
        }

        private void ShowLeaderBoard() {
            UIManager.instance.ShowLeaderboardScreen();
        }
    }
}

