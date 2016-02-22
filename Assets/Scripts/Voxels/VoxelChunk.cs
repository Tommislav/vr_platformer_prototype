using UnityEngine;
using System.Collections;

public class VoxelChunk : MonoBehaviour {

	public GameObject Block;
	private int[,,] Data;
	private GameObject[,,] Blocks;

	private int w = 15;
	private float blockSize = 4f;

	private void Awake() {
		

		Data = new int[w, w, w];
		Blocks = new GameObject[w, w, w];
		
		float off = -w * blockSize / 2f;
		Vector3 offset = new Vector3(off, off, off);
		
		for (int z=0; z< w; z++) {
			for (int x = 0; x < w; x++) {

				int h = Mathf.Max(x + z / 2, 1);

				for (int y = 0; y < w; y++) {

					int solid = 1;

					//int solid = w - y < h ? 1 : 0;
					Data[z, y, x] = solid;
					if (solid == 1) {
						GameObject block = GameObject.Instantiate(Block);
						block.transform.SetParent(transform);
						block.transform.localPosition = new Vector3(x, -y, z) * blockSize;

						Blocks[z, y, x] = block;
					}

				}
			}
		}
	}

	public Vector3 GetTileFromPos(Vector3 pos) {
		int x = Mathf.RoundToInt(pos.x / blockSize);
		int y = -Mathf.RoundToInt(pos.y / blockSize);
		int z = Mathf.RoundToInt(pos.z / blockSize);
		return new Vector3(x, y, z);
	}

	public void RemoveTile(int x, int y, int z) {
		if (x >= 0 && x < w && y >= 0 && y < w && z >= 0 && z < w) {
			if (Data[z, y, x] == 1) {
				Data[z, y, x] = 0;
				Blocks[z, y, x].SetActive(false);
			}
		}
	}

	public void Explode(Vector3 pos, int rad) {

		Vector3 origin = GetTileFromPos(pos);
		rad++;
		int minX = Mathf.Clamp((int)origin.x - rad, 0, w);
		int maxX = Mathf.Clamp((int)origin.x + rad, 0, w);
		int minY = Mathf.Clamp((int)origin.y - rad, 0, w);
		int maxY = Mathf.Clamp((int)origin.y + rad, 0, w);
		int minZ = Mathf.Clamp((int)origin.z - rad, 0, w);
		int maxZ = Mathf.Clamp((int)origin.z + rad, 0, w);

		rad--;

		float explosionLength = rad * blockSize;
		for (int z = minZ; z < maxZ; z++) {
			for (int y = minY; y < maxY; y++) {
				for (int x = minX; x < maxX; x++) {
					if (Data[z, y, x] == 1) {
						GameObject block = Blocks[z, y, x];
						float len = (pos - block.transform.position).magnitude;
						if (len < explosionLength) {
							// boom
							Data[z, y, x] = 0;
							block.SetActive(false);
						}
					}
				}
			}
		}


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
