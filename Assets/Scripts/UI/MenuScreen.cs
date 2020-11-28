using System;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Game;
using Sirenix.OdinInspector;

namespace UI {
    public class MenuScreen : MonoBehaviour {

        [SerializeField]
         private Button _playButton;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue[] _currentScoreCar;

        private void Awake() {

            _currentScore.value = 0;
            _currentScoreCar[0].value = 0;
            _currentScoreCar[1].value = 0;
            _currentScoreCar[2].value = 0;
            _playButton.onClick.AddListener(OnPlayButtonClick);

        }

        public void OnPlayButtonClick() {

            UIManager.Instance.LoadGameplay();

        }
    }
}