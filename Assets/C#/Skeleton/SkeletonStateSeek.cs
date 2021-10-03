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
		var lookDirection = blackboard.tracker.velocity;
		lookDirection.y = 0;
		transform.root.forward = Vector3.Lerp(transform.root.forward, lookDirection, Time.deltaTime * 10);

		// get real speed
		var realSpeed = blackboard.tracker.velocity.magnitude;

		// animate
		blackboard.anim.SetFloat("Time Scale", realSpeed / blackboard.agent.speed);
	}



	public override void StartState()
	{
		// start
		blackboard.agent.isStopped = false;

		startTime = Time.time;

		// animate
		blackboard.anim.SetTrigger("Walk");
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		// player dead
		if(blackboard.player.GetComponent<Health>().hitPoints <= 0)
		{
			return blackboard.idleState;
		}

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
