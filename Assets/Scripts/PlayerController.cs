using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject ShadowPrefab;
	public BombScript BombInstance;

	public float moveScaler = 10f;
	public float jumpStr = 500f;
	public float maxSpeed = 12f;
	public float decel = 0.92f;

	private Rigidbody _rb;
	private bool _isJumping;
	private float _distToGround;
	private bool _jumpBtnDown;
	private float floorY = 0f;

	private GameObject _trackerMarker;

	void Start () {
		_rb = GetComponent<Rigidbody>();
		_distToGround = GetComponent<Collider>().bounds.extents.y;
		_trackerMarker = GameObject.Find("Marker");
	}


	private bool IsGrounded() {
		if (RaycastToFloorAt(0, 0)) { // CENTER
			return true;
		}
		if (RaycastToFloorAt(-0.5f, -0.5f)) {
			return true;
		}
		if (RaycastToFloorAt(-0.5f, 0.5f)) {
			return true;
		}
		if (RaycastToFloorAt(0.5f, -0.5f)) {
			return true;
		}
		if (RaycastToFloorAt(0.5f, 0.5f)) {
			return true;
		}
		return false;
	}

	private bool RaycastToFloorAt(float offX, float offZ) {

		

		Vector3 v = new Vector3(transform.position.x + offX, transform.position.y, transform.position.z + offZ);
		return Physics.Raycast(v, Vector3.down, _distToGround + 0.001f);
	}





	void Update () {

		//_trackerMarker.SetActive(!OVRManager.tracker.isPositionTracked);

		float h = Input.GetAxis("Horizontal") * moveScaler;
		float v = Input.GetAxis("Vertical") * moveScaler;


		bool isMovingH = Math.Abs(h) > 0.1f;
		bool isMovingV = Math.Abs(v) > 0.1f;

		float vX = _rb.velocity.x;
		float vY = _rb.velocity.y;
		float vZ = _rb.velocity.z;


		if (isMovingH) {
			vX += h;
			vX = Range(vX, maxSpeed);
		} else {
			vX *= decel;
		}

		if (isMovingV) {
			vZ += v;
			vZ = Range(vZ, maxSpeed);
		} else {
			vZ *= decel;
		}



		// Jumping
		bool isGrounded = IsGrounded();

		if (_isJumping) {
			if (isGrounded) {
				Debug.Log("Back on ground!");
				_isJumping = false;
			}
		}



		bool canJump = isGrounded && !_isJumping && !_jumpBtnDown;

		if (Input.GetButton("Jump")) {
			if (canJump) {
				_jumpBtnDown = true;
				_isJumping = true;
				vY += jumpStr;

				Debug.Log("JUMPING!!!!!");
			}
			
		} else {
			_jumpBtnDown = false;
		}

		if (Input.GetButtonDown("Fire2")) {
			// BOMB
			BombInstance.PlaceBombAtPos(transform.position);
		}

		_rb.velocity = new Vector3(vX, vY, vZ);




		float fireH = Input.GetAxis("RightH");
		float fireV = Input.GetAxis("RightV");
		if (fireH == -1) {
			SendMessage("FireBulletL");
		} else if (fireH == 1) {
			SendMessage("FireBulletR");
		} else if (fireV == -1) {
			SendMessage("FireBulletD");
		} else if (fireV == 1) {
			SendMessage("FireBulletU");
		}
		


		//transform.rotation = isGrounded ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 45, 0);
	}

	
	void OnDrawGizmos() {
		
	}


	private float Range(float value, float max) {
		if (value > max) return max;
		if (value < -max) return -max;
		return value;
	}
}
