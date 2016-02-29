using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {


	public float maxSwing = 20f;
	public float add = 0.01f;
	public float startOff = 0f;


	private Transform[] parts;

	private float rad = 0f;


	void Start () {

		rad = Mathf.PI * 2f * startOff;

		parts = new Transform[transform.childCount];
		for (int i=0; i<transform.childCount; i++) {
			parts[i] = transform.GetChild(i);
		}
	}
	
	void Update () {

		rad += add;
		float rot = Mathf.Sin(rad) * maxSwing;
		
		for (int i=0; i<parts.Length; i++) {
			Transform thisTrans = parts[i];
			Transform lastTrans = i > 0 ? parts[i - 1] : null;

			float r = rot + (lastTrans == null ? 0 : lastTrans.localEulerAngles.z);
			thisTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, r));

			if (lastTrans != null) {
				thisTrans.position = lastTrans.position + (-lastTrans.up * 1f);
			}
		}


		

	}
}
