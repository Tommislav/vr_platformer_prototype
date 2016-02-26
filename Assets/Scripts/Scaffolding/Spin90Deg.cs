using UnityEngine;
using System.Collections;

public class Spin90Deg : MonoBehaviour {

	private float rot = 0f;


	void Start () {
		rot = transform.rotation.eulerAngles.y;
		OnRotDone();
	}

	private void OnRotDone() {
		rot += 90f;
		rot %= 360;
		LeanTween.rotate(gameObject, new Vector3(0, rot, 0), 2f).setDelay(2f).setOnComplete(OnRotDone);

	}


	void OnDestroy() {
		LeanTween.cancel(gameObject);
	}

}
