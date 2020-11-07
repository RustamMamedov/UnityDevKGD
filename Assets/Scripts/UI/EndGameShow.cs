﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
namespace UI {
    public class EndGameShow : MonoBehaviour {

        [SerializeField]
        private EventListener _saveEventListener;

        private void Awake() {
            _saveEventListener.OnEventHappened += ShowLeaderboardScreen;
        }

        private void ShowLeaderboardScreen() {
            UIManager.Instance.ShowLeaderboardScreen();
        }
        
    }
}
