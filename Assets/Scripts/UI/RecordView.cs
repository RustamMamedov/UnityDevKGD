using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField] 
        private Text _placeNumberLabel;
        
        [SerializeField] 
        private Text _dateLabel; 
        
        [SerializeField] 
        private Text _scoreNumberLabel;
        
        [SerializeField] 
        private GameObject _currentScoreHighlighter;

        public void SetData(int place, string date, string score, bool isCurrent) {
            _placeNumberLabel.text = place.ToString();
            _dateLabel.text = date;
            _scoreNumberLabel.text = score;
            _currentScoreHighlighter.SetActive(isCurrent);
        }
    }
}