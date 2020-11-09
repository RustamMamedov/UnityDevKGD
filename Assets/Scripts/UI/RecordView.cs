using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class RecordView : MonoBehaviour {

        // Fields.

        [SerializeField]
        private Text _positionText;
        
        [SerializeField]
        private Text _dateText;

        [SerializeField]
        private Text _scoreText;
        
        [SerializeField]
        private GameObject _distinguishedObject;


        // Methods.

        public void SetData(int position, DateTime dateTime, int score, bool distinguish = false) {
            _positionText.text = position.ToString();
            _dateText.text = dateTime.ToString("dd.MM.yyyy HH:mm");
            _scoreText.text = score.ToString();
            _distinguishedObject.SetActive(distinguish);
        }


    }

}
