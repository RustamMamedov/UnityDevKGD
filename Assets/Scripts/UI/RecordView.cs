using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private List<Text> _textLables;

        public Image BG;

        public void SetData(int pos, string date, string score) {
            _textLables[0].text=pos.ToString();
            _textLables[1].text = date;
            _textLables[2].text = score;
        }
    }
}
