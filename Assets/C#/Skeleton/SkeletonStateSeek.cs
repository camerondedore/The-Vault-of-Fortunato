using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateSeek : SkeletonState
{

	[SerializeField]
	float attackRange = 1.5f,
		attackDelay = 1;
	float startTime;



	public override void RunState()
	{
		// if(blackboard.agent.path.corners.Length < 0)
		// {
		// 	blackboard.agent.Move((blackboard.player.position - transform.root.position).normalized * blackboard.agent.speed * Time.deltaTime);
		// }

		// set destination
		blackboard.agent.destination = blackboard.player.position;

		// look
		transform.root.forward = Vector3.Lerp(transform.root.forward, blackboard.tracker.velocity, Time.deltaTime * 10);
	}



	public override void StartState()
	{
		// start
		blackboard.agent.isStopped = false;

		startTime = Time.time;
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		// check distance
		var close = Vector3.Distance(blackboard.player.position, transform.root.position) < attackRange;

		if(close && Time.time > startTime + attackDelay)
		{
			// idle
			return blackboard.attackState;
		}

		return this;
	}
}
