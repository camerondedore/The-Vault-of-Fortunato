using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJump : CharacterState
{
   
   	[SerializeField]
	float jumpResponseSpeed = 8,
		jumpHeight = 2;
	//bool queueBoost = true;



	public override void RunState()
	{
		if(blackboard.feet.headBump && blackboard.y > 0)
		{
			blackboard.y = 0;
			//queueBoost = false;
		}

		// get input
		var moveDir = Camera.main.transform.TransformDirection(blackboard.input.moveDirection);
		moveDir.y = 0;
		moveDir.Normalize();
		blackboard.targetVelocity = moveDir * blackboard.speed;

		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, Time.fixedDeltaTime * jumpResponseSpeed);

		// move
		if(blackboard.feet.isGrounded && blackboard.y < 0 && blackboard.feet.angle > blackboard.maxSlope)
		{
			// project fall if rubbing against near-vertical wall
			var originalMovement = (blackboard.velocity + Physics.gravity.normalized * -blackboard.y);
			var deflectedMovement = Vector3.ProjectOnPlane(originalMovement , blackboard.feet.checkFeet.normal);
			//deflectedMovement = deflectedMovement.normalized * originalMovement.magnitude;
			blackboard.agent.Move(deflectedMovement * Time.fixedDeltaTime);
		}
		else
		{
			blackboard.agent.Move((blackboard.velocity + Physics.gravity.normalized * -blackboard.y) * Time.fixedDeltaTime);
		}

		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.character.forward = Vector3.Slerp(blackboard.character.forward, blackboard.lookDirection, Time.fixedDeltaTime * blackboard.lookSpeed);

		// animate
		//blackboard.anim.SetFloat("y", blackboard.y);

		// check jump input for boost
		// if(PlayerInput.jump <= 0)
		// {
		// 	queueBoost = false;
		// }

		// apply acceleration due to gravity
		blackboard.y -= Mathf.Abs(Physics.gravity.y) * Time.fixedDeltaTime;
	}



	public override void StartState()
	{
		// reset queue boost
		//queueBoost = true;

		// set vertical speed; v = (-2hg)>(1/2)
		blackboard.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);

		// animate
		//blackboard.anim.SetTrigger("jump");

		// set tics
		blackboard.jumpStartAltitude = transform.root.position.y;

		// sound
		//blackboard.charAud.PlayJump();

		// set look y
		blackboard.cameraPivotController.pauseY = true;
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{
		if(blackboard.jumpStartAltitude + jumpHeight <= transform.root.position.y || blackboard.y <= 0)
		{
			// if(queueBoost)
			// {
			// 	// jump boost
			// 	return blackboard.jumpBoostState;
			// }

			// fall
			return blackboard.fallState;
		}		

		return this;
	}
}
