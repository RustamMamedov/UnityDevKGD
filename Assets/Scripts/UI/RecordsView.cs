using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RecordsView : MonoBehaviour {

        [SerializeField]
        private Text _score, _data, _place;

        public void SetData(int place, string date, string score) {

            _data.text = date;
            _score.text = score;
            _place.text = place.ToString();

        }
    }
}

