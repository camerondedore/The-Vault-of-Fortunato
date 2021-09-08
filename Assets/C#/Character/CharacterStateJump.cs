using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJump : CharacterState
{
   
   	[SerializeField]
	float jumpResponseSpeed = 8,
		jumpHeight = 2;



	public override void RunState()
	{
		if(blackboard.feet.headBump && blackboard.y > 0)
		{
			blackboard.y = 0;
		}

		// get input
		var moveDir = Camera.main.transform.TransformDirection(blackboard.input.moveDirection);
		moveDir.y = 0;
		moveDir = Vector3.ClampMagnitude(moveDir, 1);
		blackboard.targetVelocity = moveDir * blackboard.speed;

		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, Time.deltaTime * jumpResponseSpeed);

		// move
		if(blackboard.feet.isGrounded && blackboard.y < 0 && blackboard.feet.angle > blackboard.maxSlope)
		{
			// project fall if rubbing against near-vertical wall
			var originalMovement = (blackboard.velocity + Physics.gravity.normalized * -blackboard.y);
			var deflectedMovement = Vector3.ProjectOnPlane(originalMovement , blackboard.feet.checkFeet.normal);
			//deflectedMovement = deflectedMovement.normalized * originalMovement.magnitude;
			blackboard.agent.Move(deflectedMovement * Time.deltaTime);
		}
		else
		{
			blackboard.agent.Move((blackboard.velocity + Physics.gravity.normalized * -blackboard.y) * Time.deltaTime);
		}

		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.characterMesh.forward = Vector3.Slerp(blackboard.characterMesh.forward, blackboard.lookDirection, Time.deltaTime * blackboard.lookSpeed);

		// apply acceleration due to gravity
		blackboard.y -= Mathf.Abs(Physics.gravity.y) * Time.deltaTime;
	}



	public override void StartState()
	{
		// set vertical speed; v = (-2hg)>(1/2)
		blackboard.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);

		// animate
		blackboard.anim.SetTrigger("Jump");

		// set tics
		blackboard.jumpStartAltitude = transform.root.position.y;

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
			// fall
			return blackboard.fallState;
		}		

		return this;
	}
}
