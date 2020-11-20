using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _wasSaved;

        private void OnEnable() {
            SubscribeToEvents();
        }
        private void OnDisable() {
            UnsubscribeToEvents();
            RenderManager.Instance.ReleaseTextures();
        }
        private void SubscribeToEvents() {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        private void UnsubscribeToEvents() {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        private void OnCarCollision() {
            UnsubscribeToEvents();
            while (_wasSaved.value != 1) ;
            UIManager.Instance.ShowLeaderboardsScreen();
            _wasSaved.value = 0;
        }
    }
}