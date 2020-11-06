﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField]
        private Text _place;

        [SerializeField]
        private Text _date;

        [SerializeField]
        private Text _score;
        
        public void SetData(int place,string date, string score)
        {
            _place.text = place.ToString();
            _date.text = date;
            _score.text = score;
        }
    }
}
