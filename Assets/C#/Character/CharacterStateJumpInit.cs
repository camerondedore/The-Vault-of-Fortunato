using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJumpInit : CharacterState
{
   
   	[SerializeField]
	float jumpReponseSpeed = 8;
	float tics = 0;



	public override void RunState()
	{
		// get input
		var moveDir = Camera.main.transform.TransformDirection(blackboard.input.moveDirection);
		moveDir.y = 0;
		moveDir = Vector3.ClampMagnitude(moveDir, 1);

		// project input on ground
		if(blackboard.feet.isGrounded)
		{
			// grounded
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeet.normal) * blackboard.speed;
		}
		else if(blackboard.feet.isGroundedRay)
		{
			// ray grounded 
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeetRay.normal) * blackboard.speed;
		}

		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, Time.deltaTime * jumpReponseSpeed);

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

		// run tic
		tics--;
	}



	public override void StartState()
	{
		// animate
		blackboard.anim.SetTrigger("Jump Init");

		// set tics
		tics = 4;

		// sound
		blackboard.charAud.PlayJump();
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		if(tics <= 0)
		{
			// jump
			return blackboard.jumpState;
		}

		return this;
	}
}
