using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateSeek : SkeletonState
{





	public override void RunState()
	{
		// if(blackboard.agent.path.corners.Length < 0)
		// {
		// 	blackboard.agent.Move((blackboard.player.position - transform.root.position).normalized * blackboard.agent.speed * Time.deltaTime);
		// }

		// set destination
		blackboard.agent.destination = blackboard.player.position;
	}



	public override void StartState()
	{
		// start
		blackboard.agent.isStopped = false;
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		return this;
	}
}
