using UnityEngine;
using System.Collections;

public class VoxelChunk : MonoBehaviour {

	public GameObject Block;
	private int[,,] Data;


	private void Awake() {
		
		int w = 15;

		Data = new int[w, w, w];
		float blockSize = 4f;

		float off = -w * blockSize / 2f;
		Vector3 offset = new Vector3(off, off, off);

		int cnt = 0;

		for (int z=0; z< w; z++) {
			for (int y = 0; y < w; y++) {
				for (int x = 0; x < w; x++) {

					float rnd = Random.Range(0f, 1f);
					int solid = rnd < 0.2f ? 1 : 0;
					Data[z, y, x] = solid;
					if (solid == 1) {
						GameObject block = GameObject.Instantiate(Block);
						block.transform.SetParent(transform);
						block.transform.localPosition = new Vector3(x, y, z) * blockSize + offset;
					}


				}
			}
		}
	}
}
