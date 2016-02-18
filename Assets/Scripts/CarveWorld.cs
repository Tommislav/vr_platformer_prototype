using UnityEngine;
using System.Collections;

public class CarveWorld : MonoBehaviour {

	public VoxelChunk World;
	
	void Update () {
		if (Input.GetButtonDown("Fire2")) {
			World.Explode(transform.localPosition);
		}
	}
}
