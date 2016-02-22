using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public float FuseTimeSeconds = 8f;
	public Material ActiveMaterial;
	public Material InactiveMaterial;
	public VoxelChunk World;

	private bool isActive;
	private float blinkTime = 0f;
	private float fuseTime = 0f;


	private MeshRenderer mr;
	private GameObject light;
	

	private void Awake() {
		mr = GetComponent<MeshRenderer>();
		light = transform.Find("Light").gameObject;

		gameObject.SetActive(false);
	}


	public void PlaceBombAtPos(Vector3 pos) {
		this.transform.position = pos;
		blinkTime = 0f;
		fuseTime = 0f;
		gameObject.SetActive(true);
	}

	
	
	// Update is called once per frame
	void Update () {

		blinkTime += Time.deltaTime;
		if (blinkTime > 0.3f) {
			blinkTime = 0f;
			SetBlinkActive(!isActive);
		}

		fuseTime += Time.deltaTime;
		float ry = transform.localEulerAngles.y + (fuseTime * fuseTime);
		transform.localEulerAngles = new Vector3(0, ry, 0);

		if (fuseTime > FuseTimeSeconds) {
			// kaboom
			gameObject.SetActive(false);
			World.Explode(transform.position, 4);
		}
	}

	private void SetBlinkActive(bool act) {
		isActive = act;
		mr.material = isActive ? ActiveMaterial : InactiveMaterial;
		light.SetActive(isActive);

	}
}
