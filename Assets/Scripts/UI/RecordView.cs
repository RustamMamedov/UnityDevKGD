using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class RecordView : MonoBehaviour {

        [SerializeField]
        private Text _placeText;
        
        [SerializeField]
        private Text  _dateText;
        
        [SerializeField]
        private Text _scoreText;

        [SerializeField] 
        private GameObject _currentRideShow;

        public void SetData(int position, string date, int score, bool currentRide = false) {
            _placeText.text = position.ToString();
            _dateText.text = date;
            _scoreText.text = score.ToString();
            
            _currentRideShow.SetActive(currentRide);
        }
    }
}

