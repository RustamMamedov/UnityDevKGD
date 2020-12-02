using UnityEngine;
using UnityEngine.UI;

namespace Audio {

    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour {

        [SerializeField]
        private AudioManager _audioManager;

        [SerializeField]
        private Button _button;
        
        private void OnEnable() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() {
            _audioManager.PlayButtonSound();
        }
    }
}
