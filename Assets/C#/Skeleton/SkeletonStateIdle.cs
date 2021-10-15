using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateIdle : SkeletonState
{

	[SerializeField]
	float aggroRange = 4,
		verticalAggroRange = 2;



	public override void RunState()
	{
		
	}



	public override void StartState()
	{
		// stop
		blackboard.agent.isStopped = true;

		// idle
		blackboard.anim.SetTrigger("Idle");
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		var distanceToPlayer = Vector3.Distance(transform.root.position, blackboard.player.position);
		var verticalDistanceToPlayer = Mathf.Abs(transform.root.position.y - blackboard.player.position.y);

		if(blackboard.player.GetComponent<Health>().hitPoints > 0 && distanceToPlayer < aggroRange && verticalDistanceToPlayer < verticalAggroRange)
		{
			// alert
			return blackboard.alertState;
		}

		return this;
	}
}
