using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	public float maxSwing = 80f;
	public float add = 0.1f;
	public float startOff = 0f;

	private float rad = 0f;

	void Start () {
		rad = startOff;
	}
	
	void Update () {
		rad += add;
		float rot = Mathf.Sin(rad) * maxSwing;
		transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rot));
	}
}
