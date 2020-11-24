using UnityEngine;
using Events;

namespace UI {

    public class LeaderBoardShower : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionListener;

        private void OnEnable() {
            _carCollisionListener.OnEventHappened += ShowLeaderBoard;
        }

        private void OnDisable() {
            _carCollisionListener.OnEventHappened -= ShowLeaderBoard;
        }

        private void ShowLeaderBoard() {
            UIManager.instance.ShowLeaderboardScreen();
        }
    }
}

