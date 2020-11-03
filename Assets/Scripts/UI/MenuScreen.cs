using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField] 
        private Button _startCasualModeButton;
        
        [SerializeField] 
        private Button _startCrazyModeButton;
        
        [SerializeField] 
        private ScriptableBoolValue _crazyModeEnabled;

        private void Awake() {
            _startCasualModeButton.onClick.AddListener(OnCasualModeButtonClick);
            _startCrazyModeButton.onClick.AddListener(OnCrazyModeButtonClick);
        }

        private void OnCasualModeButtonClick() {
            _crazyModeEnabled.value = false;
            UIManager.Instance.LoadGameplay();
        }
        
        private void OnCrazyModeButtonClick() {
            _crazyModeEnabled.value = true;
            UIManager.Instance.LoadGameplay();
        }
    }
}