using UnityEngine;
using System.Collections;


/**
	Will search for object(s) with specific tag and notify when in line of sight
	Callbacks:
		OnLineOfSightFound(Transform)
		OnLineOfSightLost(Transform)
*/
public class LineOfSight : MonoBehaviour {

	private const float OUT_OF_RANGE = 99999f;

	public string TargetTag;
	public float Radius;
	public bool RestrictedY; // don't look up or down
	public float LookYOffset;
	public int SearchInterval = 30; // don't do GameObject.Find every frame

	private int _searchIntervalCounter;
	private Transform _currentTransform;


	public Transform getCurrentTransform() {
		return _currentTransform;
	}

	void Update() {

		float currentDist = OUT_OF_RANGE;

		if (_currentTransform != null) {
			// if we are tracking someone, check every frame if target moved out of visibility

			currentDist = GetDistanceFrom(_currentTransform);
			//Debug.Log("currentDist > " + currentDist + ", radius: " + Radius + ", outOfRange: " + (currentDist > Radius));
			if (currentDist > Radius) {
				LostSightOfTarget();
			}

		}

		_searchIntervalCounter++;
		if (_searchIntervalCounter >= SearchInterval) {
			_searchIntervalCounter = 0;

			GameObject[] objs = GameObject.FindGameObjectsWithTag(TargetTag);
			Transform closest = _currentTransform;

			foreach (GameObject go in objs) {

				if (!go.transform.Equals(_currentTransform)) {

					float d = GetDistanceFrom(go.transform);
					if (d < currentDist) {
						LostSightOfTarget();

						currentDist = d;
						closest = go.transform;
					}
				}
			}

			if (currentDist < Radius) {
				if (closest != _currentTransform && closest != null) {
					_currentTransform = closest;
					SendMessage("OnLineOfSightFound", _currentTransform, SendMessageOptions.DontRequireReceiver);
				}
			}
		}

	}



	private float GetDistanceFrom(Transform other) {

		if (RestrictedY) {
			float diffY = other.position.y - transform.position.y;
			if (-diffY < -LookYOffset || diffY > LookYOffset) {
				return OUT_OF_RANGE;
			}
		}

		Vector3 offset = other.position - transform.position;
		float dist = offset.magnitude;

		return (dist < Radius) ? dist : OUT_OF_RANGE;
	}


	private void LostSightOfTarget() {
		Debug.Log("LOST SIGHT OF TARGET!");
		SendMessage("OnLineOfSightLost", _currentTransform, SendMessageOptions.DontRequireReceiver);
		_currentTransform = null;
	}
}
