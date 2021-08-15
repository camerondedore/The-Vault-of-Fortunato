using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateFall : CharacterState
{

	//[SerializeField]
	//ParticleSystem fallDust;
	[SerializeField]
	float fallResponseSpeed = 3;
		//fallDustHeight = 4;
	float startAltitude;



	public override void RunState()
	{
		if(blackboard.feet.headBump && blackboard.y > 0)
		{
			blackboard.y = 0;
		}

		// get input
		var moveDir = Camera.main.transform.TransformDirection(blackboard.input.moveDirection);
		moveDir.y = 0;
		moveDir.Normalize();
		blackboard.targetVelocity = moveDir * blackboard.speed;

		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, Time.fixedDeltaTime * fallResponseSpeed);

		// apply acceleration due to gravity
		blackboard.y -= Mathf.Abs(Physics.gravity.y) * Time.fixedDeltaTime;

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
		if(blackboard.targetVelocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.targetVelocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.character.forward = Vector3.Slerp(blackboard.character.forward, blackboard.lookDirection, Time.fixedDeltaTime * blackboard.lookSpeed);

		// animate
		//blackboard.anim.SetFloat("y", blackboard.y);

		// set look y when character has fallen below where jump started
		// jump pauses Y so walking off edge will not be messed up by this
		if(blackboard.cameraPivotController.pauseY && blackboard.jumpStartAltitude > transform.position.y)
		{
			blackboard.cameraPivotController.pauseY = false;
		}
	}



	public override void StartState()
	{
		// disable steps
		blackboard.agent.stepOffset = 0;

		// animate
		//blackboard.anim.SetTrigger("fall");

		// height when starting to fall
		startAltitude = transform.position.y;
	}



	public override void EndState()
	{
		// sound
		blackboard.charAud.PlayLand();
		
		// animate
		//blackboard.anim.SetTrigger("jump");
		//blackboard.anim.ResetTrigger("fall");

		// fall dust
		//if(startAltitude - transform.position.y >= fallDustHeight )
		//{
			//fallDust.Play();
		//}
	}



	public override State Transition()
	{
		if(blackboard.feet.isGrounded && blackboard.y < 0 && (blackboard.feet.angle < blackboard.maxSlope))
		{
			if(!blackboard.feet.isFlat && !blackboard.feet.isFlatRay)
			{
				// slide
				return blackboard.slideState;
			}
						
			// grounded
			return blackboard.groundedSuperState;			
		}

		return this;
	}
}
