using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour {

	public GameObject BulletPrefab;
	private int _coolDown = 0;
	
	void Start () {
	
	}


	private int i = 0;
	
	void Update () {
		if (_coolDown > 0) {
			_coolDown--;
		}
	}


	public void FireBulletL() {
		FireABullet(-1, 0, 0, 1);
	}

	public void FireBulletR() {
		FireABullet(1, 0, 0, 1);
	}

	public void FireBulletU() {
		FireABullet(0, 0, -1, 1);
	}

	public void FireBulletD() {
		FireABullet(0, 0, 1, 1);
	}

	public void FireBulletAt(Vector3 v, float speed) {
		FireABullet(v.x, v.y, v.z, speed);
	}


	private void FireABullet(float x, float y, float z, float speed) {

		if (_coolDown > 0) {
			return;
		}
		_coolDown = 10;

		GameObject bullet = Instantiate(BulletPrefab) as GameObject;
		bullet.transform.position = transform.position + new Vector3(Random.value * 0.2f, Random.value * 0.2f, Random.value * 0.2f);

		Bullet bulletScript = bullet.GetComponent<Bullet>();

		float deviation = 0.05f;
		float rX = Random.Range(-deviation, deviation);
		float rZ = Random.Range(-deviation, deviation);

		bulletScript.Fire(x + rX, y, z + rZ, speed);

	}
}
