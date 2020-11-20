using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private Button _playButton;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }
        public void OnPlayButtonClick() {
            _currentScore.value = 0;
            UIManager.Instance.LoadGameplay();
        }
    }
}
