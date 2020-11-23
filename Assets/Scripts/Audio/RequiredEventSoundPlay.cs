using System.Collections;
using UnityEngine;
using Events;
using Audio;

namespace Game {

    public class RequiredEventSoundPlay : MonoBehaviour {

        [SerializeField]
        private EventListener _requiredEventListener;

        [SerializeField]
        private AudioSourcePlayer _source;

        private bool _canPlay = true;

        private void OnEnable() {
            _requiredEventListener.OnEventHappened += PlaySound;
        }
        private void OnDisable() {
            _requiredEventListener.OnEventHappened -= PlaySound;
        }


        private void PlaySound() {
            if(_canPlay == true) {
                _source.Play();
                StartCoroutine(CoolDown());
            }
        }

        IEnumerator CoolDown() {
            _canPlay = false;
            yield return new WaitForSeconds(1f);
            _canPlay = true;
        }
    }
}
