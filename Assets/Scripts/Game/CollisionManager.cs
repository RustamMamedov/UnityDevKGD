using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;
using UnityEditor;

public class CollisionManager : MonoBehaviour {
    [SerializeField]
    private EventListener _CollisionListener;
    [SerializeField]
    private GameObject _leaderBoard;



    

    private void OnEnable() {
        _CollisionListener.OnEventHappened += OnCarCollision;
    }

    private void OnDisable() {
        _CollisionListener.OnEventHappened -= OnCarCollision;
    }
    void OnCarCollision() {

        _leaderBoard.SetActive(true);
        
    }
}
