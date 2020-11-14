using UnityEngine;
using Events;

namespace UI {

    public class LeaderBoardShower : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionListener;

        private void OnEnable() {
            _carCollisionListener.OnEventHappened += ShowLeadderBoard;
        }

        private void ShowLeadderBoard() {
            UIManager.instance.ShowLeaderboardScreen();
        }
    }
}

