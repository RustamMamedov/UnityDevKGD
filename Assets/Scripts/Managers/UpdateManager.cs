using Events;
using UnityEngine;

namespace Game {
    public class UpdateManager : MonoBehaviour {
        [SerializeField]
        private GameObject _menuScreen;

        [SerializeField]
        private GameObject _gameScreen;

        [SerializeField]
        private GameObject _leaderboardsScreen;

        [SerializeField]
        private ScriptableEvent _updateEvent;

        [SerializeField]
        private ScriptableEvent _fixedEvent;

        private void Update() {
            _updateEvent.Dispatch();
        }

        private void FixedUpdate() {
            _fixedEvent.Dispatch();
        }

        public void ShowMenuScreen(){
            _menuScreen.SetActive(!(_menuScreen.activeSelf));
        }

        public void ShowGameScreen(){
            _gameScreen.SetActive(!(_gameScreen.activeSelf));
        }

        public void ShowLeaderboardsScreen(){
            _leaderboardsScreen.SetActive(!(_leaderboardsScreen.activeSelf));
        }

        public void HideAllScreens(){
            _menuScreen.SetActive(false);
            _gameScreen.SetActive(false);
            _leaderboardsScreen.SetActive(false);
        }
    }
}