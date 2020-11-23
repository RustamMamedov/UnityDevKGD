using UnityEngine;
using UnityEngine.UI;

namespace Audio {

    public class AudioButton : MonoBehaviour {

        // Fields.

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;

        [SerializeField]
        private Button _button;


        // Life cycle.

        private void Awake() {
            _button.onClick.AddListener(() => _audioSourcePlayer.Play());
        }


    }

}
