using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
        
    public class ResultView : MonoBehaviour {

        [SerializeField]
        private Text _placeLabel;

        [SerializeField]
        private Text _dateLabel;

        [SerializeField]
        private Text _scoreLabel;

        [SerializeField]
        private Image _lineImage;

        public void SetData(int place, string date, string score) {
            _placeLabel.text = place.ToString() + '.';
            _dateLabel.text = date;
            _scoreLabel.text = score;
        }

        public void SetNewScoreImage() {
            _lineImage.sprite = Resources.Load <Sprite>("Sprites/CHERRY BLOSSOM/3.IMAGE/item_0_image");
        }
    }
}