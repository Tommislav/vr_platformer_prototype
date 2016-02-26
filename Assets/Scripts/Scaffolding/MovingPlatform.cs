using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float time = 2f;
	public float pause = 2f;

	private Vector3 p1;
	private Vector3 p2;
	private int count;

	void Start() {
		p1 = transform.FindChild("p1").position;
		p2 = transform.FindChild("p2").position;

		Move();
	}

	private void Move() {
		count++;
		Vector3 target = count % 2 == 0 ? p1 : p2;
		LeanTween.move(gameObject, target, time).setDelay(pause).setOnComplete(Move);
	}

	void OnDestroy() {
		LeanTween.cancel(gameObject);
	}
}
