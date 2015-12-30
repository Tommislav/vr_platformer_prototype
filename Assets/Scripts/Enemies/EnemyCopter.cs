using UnityEngine;
using System.Collections;

public class EnemyCopter : MonoBehaviour {

	private bool _alive = true;

	public float targetY = 29f;
	public float f = 0f;
	public float add = 4f;
	public float decelAdd = 0.92f;
	public float minForce = 550f;
	public float maxForce = 770f;

	private Rigidbody _rb;
	private FireBullet _shoot;

	private Transform _trackingTransform;
	private Vector3 _aimAt;
	private int _bulletCnt;
	private int _shootDelay;
	private int _reloadDelay;

	private const int SHOOT_DELAY = 10;
	private const int RELOAD_DELAY = 100;
	private const int NUM_BULLETS = 3;

	void Start () {
		_rb = GetComponent<Rigidbody>();
		_shoot = GetComponent<FireBullet>();
		f = minForce;
	}

	int i = 0;

	void FixedUpdate () {
		
		float damp = 1000f;
		float eq = -_rb.mass * Physics.gravity.y - _rb.velocity.y * damp;
		
		float pos = transform.position.y;
		if (pos < targetY) {

			f += add;

			if (f > maxForce) f = maxForce;

			_rb.AddForce(0, eq + f, 0);
		} else if (f > minForce) {
			f -= add;
			_rb.AddForce(0, eq + f, 0);
		} else {
			f = minForce;
		}
		
	}

	private void NewPatrolTarget(Vector3 pos) {
		this.targetY = pos.y;
	}


	void Update() {
		
		if (!_alive) {	return;		}

		if (_trackingTransform != null) {

			if (--_reloadDelay <= 0) {

				if (_bulletCnt <= 0) {
					_bulletCnt = NUM_BULLETS;
					_aimAt = _trackingTransform.position;
				}

				if (--_shootDelay <= 0) {

					_shootDelay = SHOOT_DELAY;
					
					Vector3 v = _aimAt - transform.position;
					v = v.normalized;
					_shoot.FireBulletAt(v, 0.2f);

					if (--_bulletCnt <= 0) {
						_reloadDelay = RELOAD_DELAY;
					} 
					
				}
			}
		}
		







	}




	private void OnLineOfSightFound(Transform t) {
		_trackingTransform = t;
		_aimAt = t.position;

		_shootDelay = SHOOT_DELAY;
		_reloadDelay = RELOAD_DELAY;
		_bulletCnt = NUM_BULLETS;
		
	}
	private void OnLineOfSightLost() {
		_trackingTransform = null;
	}

	private void OnDead() {
		_alive = false;
		_trackingTransform = null;

		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<ParticleSystem>().Play();

		GetComponent<BoxCollider>().enabled = false;

		Light light = transform.FindChild("Light").gameObject.GetComponent<Light>();
		light.intensity = 8;
		LeanTween.value(gameObject, (val => light.intensity = val), 28, 0, 3).setOnComplete(Void => Destroy(gameObject));
		
		_rb.velocity = Vector3.zero;
		_rb.isKinematic = true;
	}
}
