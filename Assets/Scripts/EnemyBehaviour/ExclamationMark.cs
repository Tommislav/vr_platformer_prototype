using UnityEngine;
using System.Collections;

public class ExclamationMark : MonoBehaviour {

	public GameObject go;

	// Use this for initialization
	void Start () {
		go.SetActive(false);
	}

	private void OnLineOfSightFound(Transform t) {
		go.SetActive(true);
	}
    
	private void OnLineOfSightLost() {
		go.SetActive(false);
	}

	private void OnDead() {
		GameObject.Destroy(go);
	}
}
