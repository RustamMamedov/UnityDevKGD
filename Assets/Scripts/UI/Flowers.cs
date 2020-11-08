using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using System.Linq;

public class Flowers : MonoBehaviour {

    [SerializeField]
    private GameObject _flower;

    private float _speedVX;

    private float _speedVY ;

    void Start() {
        _speedVY= UnityEngine.Random.Range(-3, 3);
        _speedVX = UnityEngine.Random.Range(-3, 3);
    }
    private void Update() {
        _flower.transform.Translate(_speedVX*Time.deltaTime,0,0);
        _flower.transform.Translate(0,_speedVY * Time.deltaTime, 0);

        if (Math.Abs(transform.position.x)>1f ) {
            _speedVX = -_speedVX;
        }

        if (transform.position.y >= 4f) {
            _speedVY =-_speedVY;
        }

        if(transform.position.y <= -2f) {
            _speedVY = -_speedVY;
        }
    }

}
