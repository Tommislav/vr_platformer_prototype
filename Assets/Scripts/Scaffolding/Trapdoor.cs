using UnityEngine;
using System.Collections;
using System;

public class Trapdoor : MonoBehaviour {

	private HingeJoint joint;
	private Rigidbody rb;

	private Vector3 originalPos;
	private Vector3 connAnchor;
	private Vector3 axis;
	private JointSpring spring;

	void Start () {
		joint = GetComponent<HingeJoint>();
		rb = GetComponent<Rigidbody>();


		connAnchor = joint.connectedAnchor;
		axis = joint.axis;
		spring = joint.spring;

		originalPos = transform.position;

		LeanTween.delayedCall(gameObject, 6f, StartWindUp);
	}

	private void StartWindUp() {
		rb.useGravity = false;
		rb.isKinematic = true;
		Destroy(GetComponent<HingeJoint>());

		LeanTween.rotateLocal(gameObject, Vector3.zero, 4f);
		LeanTween.move(gameObject, originalPos, 4f);

		LeanTween.delayedCall(gameObject, 8f, ExecTrapdoor);
	}

	private void ExecTrapdoor() {
		rb.isKinematic = false;
		rb.useGravity = true;
		joint = gameObject.AddComponent<HingeJoint>();

		joint.connectedAnchor = connAnchor;
		joint.axis = axis;
		joint.useSpring = true;
		joint.spring = spring;


		LeanTween.delayedCall(gameObject, 6f, StartWindUp);
	}

	private void OnDestroy() {
		LeanTween.cancel(gameObject);
	}
}
