using UnityEngine;
using System.Collections;

public class CarveWorld : MonoBehaviour {

	public VoxelChunk World;
	
	void Update () {
		if (Input.GetButtonDown("Fire2")) {
			Vector3 p = World.GetTileFromPos(transform.position);
			
			Debug.Log("My coords: " + World.GetTileFromPos(transform.position));

			//p.y += 1;
			World.RemoveTile((int)p.x+1, (int)p.y, (int)p.z);
			World.RemoveTile((int)p.x-1, (int)p.y, (int)p.z);
			World.RemoveTile((int)p.x, (int)p.y, (int)p.z-1);
			World.RemoveTile((int)p.x, (int)p.y, (int)p.z+1);

			//World.Explode(transform.localPosition);
		}
	}
}
