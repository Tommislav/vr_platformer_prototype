using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {


	public Path patrolPath;


	public float toVel = 2.5f;
	public float maxVel = 15.0f;
	public float maxForce = 40.0f;
	public float gain = 5f;

	private Rigidbody _rb;
	private Vector3 _targetPos;

	private int _currentNode;
	private int _nodeDir;


	void Start() {
		_currentNode = 0;
		_nodeDir = 1;
		_rb = GetComponent<Rigidbody>();
		_targetPos = patrolPath.GetNode(_currentNode).position;
	}


	void FixedUpdate() {
		
		Vector3 dist = _targetPos - transform.position;
		dist.y = 0; // ignore height differences
					// calc a target vel proportional to distance (clamped to maxVel)
		Vector3 tgtVel = Vector3.ClampMagnitude(toVel * dist, maxVel);
		// calculate the velocity error
		Vector3 error = tgtVel - _rb.velocity;
		// calc a force proportional to the error (clamped to maxForce)
		Vector3 force = Vector3.ClampMagnitude(gain * error, maxForce);


		// debug
		Vector3 p = transform.position;
		Vector3 v1 = new Vector3(p.x, p.y - 0.1f, p.z);
		Vector3 v2 = v1 + force;
		Debug.DrawLine(v1, v2, Color.blue);

		_rb.AddForce(force);
	}

	void Update() {
		if (Vector3.Distance(transform.position, _targetPos) < 0.2f) {
			_currentNode += _nodeDir;
			if (_currentNode <= 0) {
				_currentNode = 0;
				_nodeDir = 1;
			} else if (_currentNode >= patrolPath.NumNodes() - 1) {
				if (patrolPath.Circular) {
					_currentNode = 0;
				} else {
					_currentNode = patrolPath.NumNodes() - 1;
					_nodeDir = -1;
				}
			}
			
			_targetPos = patrolPath.GetNode(_currentNode).position;
			SendMessage("NewPatrolTarget", _targetPos, SendMessageOptions.DontRequireReceiver);
		}
	}
}
