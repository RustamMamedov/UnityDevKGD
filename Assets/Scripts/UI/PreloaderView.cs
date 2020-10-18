using UnityEngine;
using Game;
using Events;
using UnityEngine.UI;

namespace UI {


    public class PreloaderView : MonoBehaviour {

	[SerializeField]
	private ScriptableFloatValue _sceneLoadingValue;

	[SerializeField]
	private EventListener _updateListener;

	[SerializeField]
	private Image _progressImage;

	private void Awake() {
	    _updateListener.OnEventHappend += UpdateBahavior;


	}

	private void OnDestroy() {
	    _updateListener.OnEventHappend -= UpdateBahavior;
	}

	private void UpdateBahavior() {
	    _progressImage.fillAmount = _sceneLoadingValue.value;
	}
    }
}
