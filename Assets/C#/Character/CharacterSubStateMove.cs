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
		var moveDir = Camera.main.transform.TransformDirection(blackboard.input.moveDirection);
		moveDir.y = 0;
		moveDir.Normalize();

		// project input on ground
		if(blackboard.feet.isFlat)
		{
			// grounded
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeet.normal).normalized * blackboard.speed;
		}
		else if(blackboard.feet.isFlatRay)
		{
			// ray grounded 
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeetRay.normal).normalized * blackboard.speed;
		}

		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, Time.fixedDeltaTime * groundResponseSpeed);

		// set constant downward velocity
		blackboard.y = 1f;	

		// move
		blackboard.agent.Move((blackboard.velocity + Physics.gravity.normalized * blackboard.y) * Time.fixedDeltaTime);
		
		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.character.forward = Vector3.Slerp(blackboard.character.forward, blackboard.lookDirection, Time.fixedDeltaTime * blackboard.lookSpeed);

		// get real speed
		var realSpeed = blackboard.tracker.velocity.magnitude;

		// animate
		//blackboard.anim.SetFloat("speed", realSpeed);
		if(realSpeed > 0.01f)
		{
			// run
			//blackboard.anim.SetFloat("timeScale", realSpeed / blackboard.speed);
		}
		else
		{
			// idle
			//blackboard.anim.SetFloat("timeScale", 1);
		}
	}



	public override void StartState()
	{
		
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		if(blackboard.fire1Disconnector.Trip(blackboard.input.fire1))
		{
			// attack 1
			return blackboard.meleeAttack1SubState;
		}

		if(blackboard.input.moveDirection.sqrMagnitude <= 0)
		{
			// idle
			return blackboard.idleSubState;
		}		

		return this;
	}
}
