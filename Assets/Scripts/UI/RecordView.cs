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

        public void SetData(int place, string date, string score) {
            _placeLabel.text = place.ToString();
            _dateLabel.text = date;
            _scoreLabel.text = score;
        }
    }

}
