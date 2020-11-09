using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class RecordView : MonoBehaviour {

        [SerializeField]
        private Text _placeText;

        [SerializeField]
        private Text _dateText;

        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private GameObject _flower;

        public void SetData(int place, string date, string score) {
            _placeText.text = place.ToString();
            _dateText.text = date;
            _scoreText.text = score;
        }
        public void ShowFlower() {
            _flower.SetActive(true);
        }
    }

}
