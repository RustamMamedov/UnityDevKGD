using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Game;


namespace UI {
    public class RecordView : MonoBehaviour { 

            [SerializeField]
            private Text _placeLabel;

            [SerializeField]
            private Text _dateLabel;

            [SerializeField]
            private Text _scoreLabel;

            public void SetData(int place, string date, string score) {
                _placeLabel.text = place.ToString();
                _dateLabel.text = date;
                _scoreLabel.text = score;
            }

        

    }

}
