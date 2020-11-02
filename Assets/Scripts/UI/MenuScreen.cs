using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MenuScreen : MonoBehaviour {
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private void Awake() {
            _currentScore.value = 0;
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }
        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGameplay();
        }
    }
}
