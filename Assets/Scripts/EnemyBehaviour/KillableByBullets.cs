using UnityEngine;
using System.Collections;


[RequireComponent(typeof(EnemyStats))]
public class KillableByBullets : MonoBehaviour {
	
	private EnemyStats _stats;

	public void Start() {
		_stats = GetComponent<EnemyStats>();
	}


	private void OnBulletHit(Vector3 dir) {

		if (_stats.HP > 0) {
			_stats.HP -= 1;
		}
	}
}
