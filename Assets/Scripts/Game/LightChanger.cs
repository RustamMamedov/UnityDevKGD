using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class LightChanger : MonoBehaviour {

        [SerializeField]
        private GameObject DayLight;

        [SerializeField]
        private List<GameObject> NightLight = new List<GameObject>();


        private void Awake() {
            if (PlayerPrefs.GetInt(DataKeys.LIGHT_KEY) == 0) {
                DayMode();
            } else {
                NightMode();
            }
        }
        private void DayMode() {
            DayLight.SetActive(true);

            for (int i = 0; i < NightLight.Count; i++)
                NightLight[i].SetActive(false);
        }

        private void NightMode() {
            DayLight.SetActive(false);

            for (int i = 0; i < NightLight.Count; i++)
                NightLight[i].SetActive(true);
        }
    }
}
