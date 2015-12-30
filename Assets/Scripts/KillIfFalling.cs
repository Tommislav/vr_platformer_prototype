using UnityEngine;
using System.Collections;

public class KillIfFalling : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10f) {
			GameObject.DestroyImmediate(this.gameObject);
		}
	}
}
