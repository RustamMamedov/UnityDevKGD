using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Audio {

    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour {

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;

        private Button _audioBuuton;

        private void Awake() {
            _audioBuuton=GetComponent<Button>();
            _audioBuuton.onClick.AddListener(()=> { _audioSourcePlayer.Play(); }); // (сюды параметры)=>{сюды тело метода}, ()-в этом случае безымянный
        }

        //private void OnClicButton() {
        //    _audioSource.Play();
        //}


    }
}
