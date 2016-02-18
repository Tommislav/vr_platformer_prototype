using UnityEngine;
using System.Collections;

public class RestartIfFallOff : MonoBehaviour {

	public float bottomY = -150f;

	private Vector3 startPos;
	
	void Awake() {
		startPos = transform.position;
		startPos.y += 4f;
	}

	void Update () {
		if (transform.position.y < bottomY) {
			transform.position = startPos;
		}
	}
}
