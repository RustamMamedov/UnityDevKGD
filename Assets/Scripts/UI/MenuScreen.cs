using System.Collections;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField] 
        private Button _startGameModeButton;
        
        [SerializeField] 
        private Button _settingsButton;
        
        [SerializeField]
        private EventDispatcher _startGameMusicEventDispatcher;

        private void Awake() {
            _startGameModeButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }

        private void OnCasualModeButtonClick() {
            _crazyModeEnabled.value = false;
        }
        
        private void OnPlayButtonClick() {
            _startGameMusicEventDispatcher.Dispatch();
            UIManager.Instance.LoadGameplay();
        }

        private void OnSettingsButtonClick() {
            ShowSettingsScreen();
        }
        
        private void ShowSettingsScreen() {
            StartCoroutine(WaitAndShowScreen());
        }
		
        private IEnumerator WaitAndShowScreen() {
            yield return new WaitForSeconds(0.1f);
            UIManager.Instance.ShowSettingsScreen();
        }
    }
}