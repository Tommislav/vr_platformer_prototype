using UnityEngine;
using System.Collections;

public class Spin90Deg : MonoBehaviour {

	public enum Rot {
		x,y,z
	};

	public Rot RotationAxis = Rot.y;
	private float rot = 0f;

	


	void Start () {
		rot = transform.rotation.eulerAngles.y;
		OnRotDone();
	}

	private void OnRotDone() {
		rot += 90f;
		rot %= 360;

		Vector3 v = new Vector3(0, rot, 0);
		if (RotationAxis == Rot.x) {
			v = new Vector3(rot, 0, 0);
		} else if (RotationAxis == Rot.z) {
			v = new Vector3(0, 0, rot);
		}

		LeanTween.rotateLocal(gameObject, v, 2f).setDelay(2f).setOnComplete(OnRotDone);

	}


	void OnDestroy() {
		LeanTween.cancel(gameObject);
	}

}
