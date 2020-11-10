using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordView : MonoBehaviour {

        [SerializeField]
        private List<Text> _textLables;

        public Image BG;

        enum ListText {
            Plase=0,
            Date,
            Score
        }

        public void SetData(int pos, string date, string score) {
            _textLables[ListText.Plase.GetHashCode()].text=pos.ToString();
            _textLables[ListText.Date.GetHashCode()].text = date;
            _textLables[ListText.Score.GetHashCode()].text = score;
        }
    }
}
