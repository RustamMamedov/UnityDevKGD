using UnityEngine;
using Audio;

namespace Game {

    public class StarScript : MonoBehaviour {
        
        [SerializeField]
        private AudioSourcePlayer _starAudio;

        [SerializeField]
        private ScriptableFloatValue _starDistance;
    }
}