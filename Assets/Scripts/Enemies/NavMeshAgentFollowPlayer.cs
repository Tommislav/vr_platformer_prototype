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
		_agent.updateRotation = false;

		_player = GameObject.FindGameObjectWithTag("Player");
		_countdown = 0;
	}
	
	// Update is called once per frame
	void Update () {

		

		if (!_isJumping) {
			if (_agent.isOnOffMeshLink) {
				
				
				Vector3 targetPos = _agent.currentOffMeshLinkData.endPos;
				targetPos.y += 0.75f;

				if (targetPos.y < transform.position.y) {
					_agent.autoTraverseOffMeshLink = true;
					return; // falling don't need any jump behaviour
				}

				_agent.autoTraverseOffMeshLink = false;

				_isJumping = true;
				_agent.Stop();

				LeanTween.moveX(gameObject, targetPos.x, 1f).setDelay(0.2f).setEase(LeanTweenType.easeInOutSine);
				LeanTween.moveZ(gameObject, targetPos.z, 1f).setDelay(0.2f).setEase(LeanTweenType.easeInOutSine);
				LeanTween.moveY(gameObject, transform.position.y + 5f, 0.5f).setDelay(0.2f).setEase(LeanTweenType.easeOutSine);
				LeanTween.moveY(gameObject, targetPos.y, 0.3f).setDelay(1.2f).setEase(LeanTweenType.easeInSine);
				LeanTween.delayedCall(gameObject, 1.3f, () => {
					_agent.CompleteOffMeshLink();
					_agent.Resume();
					_isJumping = false;
					_countdown = 0;
				});

				return;
			}




			if (--_countdown <= 0) {
				_agent.destination = _player.transform.position;
				_countdown = 60;
			}
		}


		



		
	}
}
