﻿using Events;
using UnityEngine;
using System.Collections;
using Game;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private CarSettings[] _carSettings;

        [SerializeField]
        private CarDodgeView[] _carDodgeViews;

        private void OnEnable() {
            StartCoroutine(InitCarDodgeViews());
        }

        private IEnumerator InitCarDodgeViews() {
            for (int i = 0; i < _carSettings.Length; i++) {
                _carDodgeViews[i].Init(_carSettings[i]);
                yield return null;
            }
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}