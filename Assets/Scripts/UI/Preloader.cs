using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UI {

    public class Preloader : MonoBehaviour {

	private void Start() {
	    StartCoroutine(LoadMenuScene());
	}

	private IEnumerator LoadMenuScene() {

	    var asyncOperation = SceneManager.LoadSceneAsync("Menu");
	    while (!asyncOperation.isDone) {
		Debug.Log(asyncOperation.progress);
		yield return null;
	    }
	}
    }
}
