using UnityEngine;
using System.Collections;

public class BulletBurst : MonoBehaviour {

	public static BulletBurst Instance;

	[HideInInspector]
	public ParticleSystem ParticleSys;


	// Use this for initialization
	void Start () {
		Instance = this;
		ParticleSys = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
