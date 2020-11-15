using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private Text _placeLabel;

        [SerializeField]
        private Text _dataLabel;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private Image _imageNewRecordMark;

        public void SetData(int place, string data, string score, bool isCurrentRide) {
            _placeLabel.text = $"{place}";
            _dataLabel.text = data;
            _scoreLabel.text = score;
            _imageNewRecordMark.enabled = isCurrentRide;
        }
    }
}