using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Game {
    public class LightChange : MonoBehaviour {

        private void Awake() {
            if (Settings.Instance != null) {
                if (GetComponent<Light>().type!=LightType.Point && !Settings.Instance.IsDay) {
                    gameObject.SetActive(false);
                }
                else if(GetComponent<Light>().type == LightType.Point && Settings.Instance.IsDay) {
                    gameObject.SetActive(false);
                }
            }
            else {
                if (GetComponent<Light>().type != LightType.Point) {
                    gameObject.SetActive(false);
                }
            }
        }

    }
}

