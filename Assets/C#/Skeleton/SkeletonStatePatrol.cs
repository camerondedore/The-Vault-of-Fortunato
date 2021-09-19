using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStatePatrol : SkeletonState
{

	[SerializeField]
	float patrolRadius = 3;
	Vector3 startPosition;



	public override void RunState()
	{
		
	}



	public override void StartState()
	{
		// start
		blackboard.agent.isStopped = false;

		// get starting postiion
		if(startPosition == Vector3.zero)
		{
			startPosition = transform.root.position;
		}

		// set target position
		var newDestination = startPosition + Random.onUnitSphere * Random.Range(0f, patrolRadius);
		newDestination.y = transform.root.position.y;
		blackboard.agent.destination = newDestination;
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		if(blackboard.agent.remainingDistance < 0.1f)
		{
			// idle
			return blackboard.idleState;
		}

		return this;
	}
}
