using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Vector3 Dir;
	private float Speed;
	private int Life = 100;


	public void Fire(float x, float y, float z, float speed) {
		Speed = speed;
		Dir = new Vector3(x,y,z) * Speed;
	}

	public void Fire(float x, float z, float speed) {
		this.Fire(x, 0.0f, z, speed);
	}


	void Update () {
		if (--Life <= 0) {
			GameObject.Destroy(gameObject);
			return;
		}


		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, Dir, out hitInfo, Dir.magnitude)) {
			Vector3 hitPos = hitInfo.point;
			
			Vector3 hitNormal = hitInfo.normal;

			// instanciate particles

			BulletBurst.Instance.transform.position = hitPos;
			BulletBurst.Instance.ParticleSys.Emit(30);

			hitInfo.transform.gameObject.SendMessage("OnBulletHit", Dir, SendMessageOptions.DontRequireReceiver);

			GameObject.Destroy(gameObject);
			return;
		}



		transform.position += Dir;
	}
}
