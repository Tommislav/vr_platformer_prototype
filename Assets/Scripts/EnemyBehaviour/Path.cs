using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

	private Transform[] _nodes;
	public bool Circular;

	void Awake() {
		initNodes();
	}


	private void initNodes() {
		int n = transform.childCount;
		_nodes = new Transform[n];
		for (int i=0; i < n; i++) {
			_nodes[i] = transform.GetChild(i).transform;
		}
	}
	
	void OnDrawGizmos() {
		if (_nodes == null) { initNodes(); }

		if (_nodes.Length < 2) { return;  }
		
		Vector3 v = _nodes[0].position;
		for (int i = 1; i < _nodes.Length; i++) {
			Vector3 n = _nodes[i].position;
			Debug.DrawLine(v, n, Color.red);
			v = n;
		}
	}

	public int NumNodes() {
		return _nodes.Length;
	}

	public Transform GetNode(int n) {
		if (_nodes == null) { initNodes(); }
		return _nodes[n];
	}


}
