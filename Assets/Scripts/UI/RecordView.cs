using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private Text _placeT;

        [SerializeField]
        private Text _dateT;

        [SerializeField]
        private Text _scoreT;

        public void SetData(int position, string date, string score) {
            _placeT.text = position.ToString();
            _dateT.text = date;
            _scoreT.text = score;
        }
    }
}
