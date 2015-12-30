using UnityEngine;
using System.Collections;

public class HUD_LostTracker : MonoBehaviour {

	private Renderer _renderer;

	void Start () {
		_renderer = GetComponent<Renderer>();
		_renderer.enabled = false;
	}
	
	
	void Update () {
		if (!OVRManager.tracker.isPositionTracked && !_renderer.enabled) {
			_renderer.enabled = true;
		} else {
			_renderer.enabled = false;
		}
	}
}
