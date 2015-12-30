using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomMovement : MonoBehaviour {


	public float dist = 1.0f;

	void Start () {
	
		
		WaitForRandomMove();

	}
	
	void Update () {
		
	}


	private void MoveRandom() {
		
		
		int moveDir = (int) (Random.value*4f);
		float[] rots = new[] {0f, Mathf.PI/2f, Mathf.PI, Mathf.PI*1.5f};
		float rad = rots[moveDir];

		//float rad = ((float) rot)/180f*Mathf.PI;
		float mX = Mathf.Round(Mathf.Cos(rad)) * dist;
		float mY = transform.position.y;
		float mZ = Mathf.Round(Mathf.Sin(rad)) * dist;

		Vector3 newPos = transform.position + new Vector3(mX, 0f, mZ);

		LeanTween.move(gameObject, newPos, 1f).setOnComplete(WaitForRandomMove).setEase(LeanTweenType.easeInOutQuad);
		
	}

	private void WaitForRandomMove() {

		GetComponent<Rigidbody>().velocity = Vector3.zero;

		LeanTween.delayedCall(gameObject, 1f, MoveRandom);
	}
}
