using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateHurt : SkeletonState
{

	[SerializeField]
	float hurtTime = 0.5f;
	float startTime = 0;



	public override void RunState()
	{
		// move
		blackboard.agent.Move((-transform.root.forward) * Time.deltaTime);
	}



	public override void StartState()
	{
		// animate
		//blackboard.anim.SetTrigger("Hurt");

		startTime = Time.time;

		// sound
		//blackboard.charAud.PlayLand();
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{
		if(startTime + hurtTime < Time.time)
		{
			// grounded
			return blackboard.seekState;
		}

		return this;
	}
}
