using UnityEngine;

namespace Game {

    public class DayNightManager : MonoBehaviour {

        [SerializeField]
        private GameObject _dayLight;
        
        [SerializeField]
        private GameObject _nightLight;

        [SerializeField] 
        private ScriptableBoolValue _isNightScriptableBoolValue;
        
        private void OnEnable() {
	        _nightLight.SetActive(_isNightScriptableBoolValue.value);
			_dayLight.SetActive(!_isNightScriptableBoolValue.value);
		}
    }
}