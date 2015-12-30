using UnityEngine;
using System.Collections.Generic;

public class EnemyStats : MonoBehaviour {

	[SerializeField]
	private int _hp = 3;
	private bool _isAlive = true;

	public Dictionary<string, int> Data;

	private void Awake() {
		Data = new Dictionary<string, int>();
	}

	public bool IsAlive {
		get { return _isAlive; }
		set {
			if (value != _isAlive) {
				_isAlive = value;

				if (!_isAlive) { SendMessage("OnDead", SendMessageOptions.DontRequireReceiver); }
			}
		}
	}
	

	public int HP {
		get { return _hp; }
		set {
			_hp = value;
			if (value <= 0) { IsAlive = false; }
		}
	}


}
