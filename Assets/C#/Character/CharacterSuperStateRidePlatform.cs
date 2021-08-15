using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSuperStateRidePlatform : CharacterSuperState
{





	public override void RunState()
	{
		// parent to platform
		if(blackboard.feet.GetGround() != null && blackboard.root.parent != blackboard.feet.GetGround().transform)
		{
			blackboard.root.parent = blackboard.feet.GetGround().transform;
		}

		// center camera
		if(blackboard.input.centerCamera > 0)
		{
			blackboard.cameraController.CenterCamera();
		}

		base.RunState();
	}



	public override void StartState()
	{
		// enable steps
		blackboard.agent.stepOffset = blackboard.stepHeight;

		// start idle
		subState = blackboard.idleSubState;

		// animate
		//blackboard.anim.SetTrigger("grounded");

		// set character to be vertical
		blackboard.character.forward = Vector3.ProjectOnPlane(blackboard.character.forward, Vector3.up);

		// set look y
		blackboard.cameraPivotController.pauseY = false;

		base.StartState();
	}



	public override void EndState()
	{
		// remove parent
		blackboard.root.parent = null;

		base.EndState();
	}



	public override State Transition()
	{
		if((blackboard.feet.isFlat || blackboard.feet.isFlatRay))
		{			
			if(blackboard.jumpDisconnector.Trip(blackboard.input.jump))
			{
				// jump
				return blackboard.jumpStateInit;
			}

			var ground = blackboard.feet.GetGround();
			if(ground != null)
			{
				var platformComp = ground.transform.root.GetComponent<PlatformMoving>();

				if(platformComp == null)
				{
					// not on platform
					return blackboard.groundedSuperState;
				}
			}

			return this;
		}
		else
		{
			// fall
			return blackboard.fallState;
		}
	}
}
