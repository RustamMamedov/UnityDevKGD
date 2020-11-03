using System;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Game;

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

        public void OnPlayButtonClick() {

            UIManager.Instance.LoadGameplay();

        }
    }
}