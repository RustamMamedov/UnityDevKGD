using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
 
namespace Game { 
    public class TimeDay : MonoBehaviour { 
        [SerializeField] 
        private GameObject _gameplaylLight; 

        [SerializeField] 
        private List<GameObject> _carLight; 
 
 
        [SerializeField] 
        private ScriptableIntValue _valDay; 
 
        private void Awake() { 
            if (_valDay.value == 0) { 
                _gameplaylLight.SetActive(true);
                 for (int i = 0; i < _carLight.Count; i++) { 
                    _carLight[i].SetActive(false); 
                } 
            } 
            else { 
                _gameplaylLight.SetActive(false);
                for (int i = 0; i < _carLight.Count; i++) { 
                    _carLight[i].SetActive(true); 
                } 
            } 
        } 
 
    } 
} 