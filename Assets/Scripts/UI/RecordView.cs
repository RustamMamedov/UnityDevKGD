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

        public void SetData(int place, string data, string score) {
            _placeLabel.text = $"{place}";
            _dataLabel.text = data;
            _scoreLabel.text = score;
        }
    }
}