using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private Text _place;

        [SerializeField]
        private Text _data;

        [SerializeField]
        private Text _score;

        [SerializeField]
        private Image _imageNewScore;

        public void SetData(int place, string data, string score, bool isCurrentRide) {
            _place.text = $"{place}";
            _data.text = data;
            _score.text = score;
            _imageNewScore.enabled = isCurrentRide;
        }
    }
}