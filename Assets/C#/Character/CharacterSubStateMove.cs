using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSubStateMove : CharacterState
{
    
	[SerializeField]
	float groundResponseSpeed = 15;



	public override void RunState()
	{
		// get input
		var moveDir = blackboard.cameraPivot.TransformDirection(blackboard.input.moveDirection);
		moveDir.y = 0;
		moveDir = Vector3.ClampMagnitude(moveDir, 1);

		// project input on ground
		if(blackboard.feet.isFlat)
		{
			// grounded
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeet.normal) * blackboard.speed;
		}
		else if(blackboard.feet.isFlatRay)
		{
			// ray grounded 
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeetRay.normal) * blackboard.speed;
		}

		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, Time.deltaTime * groundResponseSpeed);

		// set constant downward velocity
		blackboard.y = 1f;	

		// move
		blackboard.agent.Move((blackboard.velocity + Physics.gravity.normalized * blackboard.y) * Time.deltaTime);
		
		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.characterMesh.forward = Vector3.Slerp(blackboard.characterMesh.forward, blackboard.lookDirection, Time.deltaTime * blackboard.lookSpeed);

		// get real speed
		var realSpeed = blackboard.tracker.velocity.magnitude;

		// animate
		blackboard.anim.SetFloat("Time Scale", realSpeed / blackboard.speed);	
	}



	public override void StartState()
	{
		// animate
		blackboard.anim.SetTrigger("Walk");
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		if(blackboard.fire1Disconnector.Trip(blackboard.input.fire1))
		{
			// attack
			return blackboard.meleeAttackSubState;
		}

		if(blackboard.input.moveDirection.sqrMagnitude <= 0.05f)
		{
			// idle
			return blackboard.idleSubState;
		}		

		return this;
	}
}
