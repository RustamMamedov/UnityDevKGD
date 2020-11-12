using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ResultView : MonoBehaviour {

        [SerializeField]
        private List<Text> _lableRecordView;

        [SerializeField]
        private Image _image;

        public void SetData(int place, string date, string score) {
            _lableRecordView[0].text = place.ToString();
            _lableRecordView[1].text = date;
            _lableRecordView[2].text = score;
        }

        public void Symbol(){
            _image.enabled = true;
        }
    }
}