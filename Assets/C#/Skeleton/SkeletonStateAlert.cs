using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateAlert : SkeletonState
{

	float startTime;



	public override void RunState()
	{
		// look
		var lookDirection = blackboard.player.position - transform.root.position;
		lookDirection.y = 0;
		transform.root.forward = Vector3.Lerp(transform.root.forward, lookDirection, Time.deltaTime * 10);
	}



	public override void StartState()
	{	
		startTime = Time.time;

		// anim
		blackboard.anim.SetTrigger("Hurt");		
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		if(startTime + 0.25f < Time.time)
		{
			// seek
			return blackboard.seekState;		
		}

		return this;
	}
}
