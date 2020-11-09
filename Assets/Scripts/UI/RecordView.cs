using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private Text _place;

        [SerializeField]
        private Text _date;

        [SerializeField]
        private Text _score;

        [SerializeField]
        private Image _imageNewScore;

        public void SetData(int place, string date, string score) {
            _place.text = place.ToString();
            _date.text = date;
            _score.text = score;
        }

        public void MarkRecord() {
            _imageNewScore.gameObject.SetActive(true);
        }

        public void UnmarkRecord() {
            _imageNewScore.gameObject.SetActive(false);
        }
    }
}