using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField] 
        private Button _startCrazyModeButton;
        
        [SerializeField] 
        private Button _startCasualModeButton;

        [SerializeField] 
        private ScriptableBoolValue _crazyMode;

        private void Awake() {
            _startCasualModeButton.onClick.AddListener(OnCasualModeButtonClick);
            _startCrazyModeButton.onClick.AddListener(OnCrazyModeButtonClick);
        }

        private void OnCasualModeButtonClick() {
            _crazyMode.value = false;
            UIManager.Instance.LoadGameplay();
        }
        
        private void OnCrazyModeButtonClick() {
            _crazyMode.value = true;
            UIManager.Instance.LoadGameplay();
        }
    }
}