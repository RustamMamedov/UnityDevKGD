using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;
using Audio;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private ScriptableIntValue _wasSaved;

        [SerializeField]
        private AudioSourcePlayer _collisionPlayer;
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
            _collisionPlayer.Play();
            while (_wasSaved.value != 1) ;
            UIManager.Instance.ShowLeaderboardsScreen();
            _wasSaved.value = 0;
        }
    }
}