using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class MenuScreen : MonoBehaviour {

        // Fields.

        [SerializeField]
        private Button _playButton;


        // Life cycle.

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }


        // Event handling.

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGameplayScene();
        }


    }

}
