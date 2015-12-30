using UnityEngine;

namespace Enemies {
	public class EnemyCharger : MonoBehaviour {
		
		public float MoveForce = 60f;
		public float MaxForce = 60f;
		
		private Rigidbody _rb;
		private bool _isMoving;

		private int _countDown;
        private float _str = 0f;

		private EnemyStats _stats;


		private Transform _lineOfSightTarget;

		void Start () {
			_rb = GetComponent<Rigidbody>();

			_stats = GetComponent<EnemyStats>();
		}
	
		private void OnLineOfSightFound(Transform t) {
			_lineOfSightTarget = t;
		}
		private void OnLineOfSightLost() {
			_lineOfSightTarget = null;
		}



		void Update () {

			if (_lineOfSightTarget != null && _stats.IsAlive) {
			
				Vector3 offset = _lineOfSightTarget.position - transform.position;
				offset.y = 0;
				float dist = offset.magnitude;
			
				_isMoving = true;

                if (_str < MaxForce) {
                    _str += MoveForce;
                }


                Vector3 f = offset.normalized * _str;
                f.y = 0f;
				_rb.AddForce(f);


			} else if (_isMoving) {

                _str = 0;
				if (Mathf.Abs(_rb.velocity.x) < 0.01f && Mathf.Abs(_rb.velocity.z) < 0.01f) {
					_rb.velocity = new Vector3(0, _rb.velocity.y, 0);
					_isMoving = false;
				}
			}
		}

		private void OnCollisionEnter(Collision coll) {
			string collName = coll.gameObject.name;
			
			if (collName.Equals("Player")) {
				Vector3 offset = transform.position - coll.transform.position;
				_countDown = 30;
			}
		}

		private void OnBulletHit(Vector3 bulletVel) {
			if (!_stats.IsAlive) { return; }
			bulletVel.y = 0.4f;
			_rb.AddForce(bulletVel * 20000f);
			_str *= 0.5f;
		}

		private void OnDead() {
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<ParticleSystem>().Play();
				
			GetComponent<BoxCollider>().enabled = false;

			Light light = transform.FindChild("Light").gameObject.GetComponent<Light>();
			light.intensity = 8;
			LeanTween.value(gameObject, (val => light.intensity = val), 28, 0, 3).setOnComplete( Void => Destroy(gameObject) );
				

			_rb.velocity = Vector3.zero;
			_rb.isKinematic = true;
		}

	}
}
