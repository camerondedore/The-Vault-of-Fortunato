using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateIntro : CharacterState
{

	[SerializeField]
	float minTime = 1,
		startTime = 0;



	public override void RunState()
	{

	}



	public override void StartState()
	{
		startTime = Time.time;
		// turn off controller
		//blackboard.cameraController.enabled = false;
		// turn off look
		//blackboard.cameraPivotController.enabled = false;
	}



	public override void EndState()
	{
		// turn on controller
		//blackboard.cameraController.enabled = true;
		// turn on look
		//blackboard.cameraPivotController.enabled = true;
	}



	public override State Transition()
	{	
		if(Time.time > minTime + startTime && (blackboard.input.jump > 0 || blackboard.input.look != Vector2.zero || blackboard.input.moveDirection != Vector3.zero))
		{
			// ground
			return blackboard.groundedSuperState;
		}		

		return this;
	}
}
