using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private List<Text> _lablesRecordView;

        [SerializeField]
        private Image _image;

        public void SetData(int place, string date, string score) {
            _lablesRecordView[0].text = place.ToString();
            _lablesRecordView[1].text = date;
            _lablesRecordView[2].text = score;
        }

        public void Symbol() {
            //_lablesRecordView[0].text = "aaa";
            _image.enabled = true;
        }
    }
}
