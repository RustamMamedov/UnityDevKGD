using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace UI {

    public class GameScreen : MonoBehaviour {


	[SerializeField]
	private EventListener _saveEventListener;

	private void Awake() {
	    _saveEventListener.OnEventHappend += OnGameSaved;
	}

	private void OnGameSaved() {
	    UIManager.Instance.ShowLeaderboardScreen();
	}

    }
}
