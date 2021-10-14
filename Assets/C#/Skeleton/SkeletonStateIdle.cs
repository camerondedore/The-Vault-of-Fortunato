using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateIdle : SkeletonState
{

	// [SerializeField]
	// Vector2 idleTimeRange;
	// float startTime,
	// 	idleTime;
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

		// time
		//startTime = Time.time;
		//idleTime = Random.Range(idleTimeRange.x, idleTimeRange.y);

		// idle
		blackboard.anim.SetTrigger("Idle");
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		// if(startTime + idleTime < Time.time)
		// {
		// 	// patrol
		// 	return blackboard.patrolState;
		// }

		var distanceToPlayer = Vector3.Distance(transform.root.position, blackboard.player.position);
		var verticalDistanceToPlayer = Mathf.Abs(transform.root.position.y - blackboard.player.position.y);

		if(blackboard.player.GetComponent<Health>().hitPoints > 0 && distanceToPlayer < aggroRange && verticalDistanceToPlayer < verticalAggroRange)
		{
			// seek
			return blackboard.seekState;
		}

		return this;
	}
}
