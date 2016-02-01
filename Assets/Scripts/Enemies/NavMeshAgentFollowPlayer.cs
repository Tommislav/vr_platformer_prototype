using UnityEngine;
using System.Collections;

public class NavMeshAgentFollowPlayer : MonoBehaviour {

	private NavMeshAgent _agent;
	private int _countdown;
	private GameObject _player;

	private bool _isJumping;

	// Use this for initialization
	void Start () {
		_agent = GetComponent<NavMeshAgent>();
		_agent.autoTraverseOffMeshLink = false;

		_player = GameObject.FindGameObjectWithTag("Player");
		_countdown = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (!_isJumping) {
			if (_agent.isOnOffMeshLink) {
				Debug.Log("I am on an offMeshLink to " + _agent.nextOffMeshLinkData.endPos + ", valid: " + _agent.nextOffMeshLinkData.valid);
				//_isJumping = true;
				//_agent.Stop();

				//Vector3 targetPos = _agent.nextOffMeshLinkData.endPos;
				//LeanTween.moveX(gameObject, targetPos.x, 1f).setDelay(0.5f).setEase(LeanTweenType.easeInOutSine);
				//LeanTween.moveZ(gameObject, targetPos.z, 1f).setDelay(0.5f).setEase(LeanTweenType.easeInOutSine);
				//LeanTween.moveY(gameObject, transform.position.y + 5f, 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutSine);
				//LeanTween.moveY(gameObject, targetPos.y, 0.5f).setDelay(1.5f).setEase(LeanTweenType.easeInSine);
				//LeanTween.delayedCall(gameObject, 2f, () => {
				//	_agent.Resume();
				//	_isJumping = false;
				//	_countdown = 0;
				//});

				//return;
			}




			if (--_countdown <= 0) {
				_agent.destination = _player.transform.position;
				_countdown = 60;
			}
		}


		



		
	}
}
