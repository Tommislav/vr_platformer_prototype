using UnityEngine;
using System.Collections;

public class VoxelChunk : MonoBehaviour {

	public GameObject Block;
	private int[,,] Data;
	private GameObject[,,] Blocks;


	private void Awake() {
		
		int w = 15;

		Data = new int[w, w, w];
		Blocks = new GameObject[w, w, w];
		float blockSize = 4f;

		float off = -w * blockSize / 2f;
		Vector3 offset = new Vector3(off, off, off);
		
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

						Blocks[z, y, x] = block;
					}

				}
			}
		}
	}

	public void Explode(Vector3 pos) {
		int w = 15;
		float blockSize = 4f;
		float offset = -w * blockSize / 2f;

		int x = Mathf.FloorToInt((pos.x - offset) / blockSize);
		int y = Mathf.FloorToInt((pos.y - offset) / blockSize);
		int z = Mathf.FloorToInt((pos.z - offset) / blockSize);

		Debug.Log("Explode at coords " + x + "," + y + "," + z);

		RemoveBlock(x - 1, y, z);
		RemoveBlock(x + 1, y, z);
		RemoveBlock(x, y, z - 1);
		RemoveBlock(x, y, z + 1);
		RemoveBlock(x, y + 1, z);
	}

	private void RemoveBlock(int x, int y, int z) {

		if (x<0 ||x>=15 || y<0 || y>=0 || z < 0 || z >= 15) {
			return;
		}

		GameObject b = Blocks[z, y, x];
		if (b != null) {
			Debug.Log("Removing block at " + x + "," + y + "," + z);
			b.SetActive(false);
		}
	}
}
