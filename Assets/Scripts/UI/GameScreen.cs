using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField]
        private EventListener _carCollisionEventListener;

        private void OnEnable()
        {
            SubscribeToEvents();
        }
        private void OnDisable()
        {
            UnsubscribeToEvents();
        }
        private void SubscribeToEvents()
        {
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        private void UnsubscribeToEvents()
        {
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        private void OnCarCollision()
        {
            UnsubscribeToEvents();
            UIManager.Instance.ShowLeaderboardsScreen();
        }
    }
}