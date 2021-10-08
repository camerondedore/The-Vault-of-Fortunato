using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateDie : CharacterState
{

	float restartDelay = 4.1f,
		deathTime = Mathf.Infinity;
	bool animated = false;



	public override void RunState()
	{
		// clear velocity
		blackboard.velocity = Vector3.zero;

		// apply acceleration due to gravity
		blackboard.y -= Mathf.Abs(Physics.gravity.y) * Time.deltaTime;	
		blackboard.y = Mathf.Clamp(blackboard.y , -10, 100);

		// move
		blackboard.agent.Move((Physics.gravity.normalized * -blackboard.y) * Time.deltaTime);

		// die animation when grounded
		if(blackboard.feet.isGrounded && !animated)
		{						
			// grounded
			animated = true;

			// animate			
			//blackboard.anim.SetTrigger("die");

			// die audio
			blackboard.charAud.PlayDie();	

			// get death time
			deathTime = Time.time;	
		}

		if(deathTime + restartDelay < Time.time)
		{
			SceneLoader.RestartLevel();
		}
	}



	public override void StartState()
	{
		// set look y
		blackboard.cameraPivotController.pauseY = false;

		// turn off all colliders
		var colliders = transform.root.GetComponentsInChildren<Collider>();
		foreach(var c in colliders)
		{
			if(c.GetType() != typeof(CharacterController))
			{
				c.enabled = false;
			}
		}

		// anim
		blackboard.anim.SetTrigger("Die");

		// fx
		blackboard.blood.Play();
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		return this;
	}
}
