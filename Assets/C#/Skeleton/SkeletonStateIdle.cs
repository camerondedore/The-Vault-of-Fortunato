using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateIdle : SkeletonState
{

	[SerializeField]
	Vector2 idleTimeRange;
	float startTime,
		idleTime;



	public override void RunState()
	{
		
	}



	public override void StartState()
	{
		// stop
		blackboard.agent.isStopped = true;

		// time
		startTime = Time.time;
		idleTime = Random.Range(idleTimeRange.x, idleTimeRange.y);
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		if(startTime + idleTime < Time.time)
		{
			// patrol
			return blackboard.patrolState;
		}

		return this;
	}
}
