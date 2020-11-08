using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ResoltView : MonoBehaviour {
        [SerializeField]
        private Text _position;

        [SerializeField]
        private Text _date;

        [SerializeField]
        private Text _score;

        public void SetData(int place,string date,string score) {
            _position.text = place.ToString();
            _date.text = date;
            _score.text = score;
        }

    }
}