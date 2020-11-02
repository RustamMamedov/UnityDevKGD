using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private ScriptableIntValue _currentScoreValue;

        [SerializeField]
        private Button _playButton;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            ResetScore();
            UIManager.Instance.LoadGameplay();
        }

        private void ResetScore() {
            _currentScoreValue.value = 0;
        }
    }
}