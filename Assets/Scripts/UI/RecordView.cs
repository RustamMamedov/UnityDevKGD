using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private Text _dateLabel;

        [SerializeField]
        private Text _placeLabel;

        [SerializeField]
        private Image _newRecordImage;

        public void SetData(int place, string date, string score) {
            _placeLabel.text = place.ToString();
            _dateLabel.text = date;
            _scoreLabel.text = score;
        }

        public void MarkRecord() {
            _newRecordImage.gameObject.SetActive(true);
        }

        public void UnmarkRecord() {
            _newRecordImage.gameObject.SetActive(false);
        }
    }

}
