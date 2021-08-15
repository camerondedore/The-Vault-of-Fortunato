using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateSlide : CharacterState
{
    
	//[SerializeField]
	//ParticleSystem feetDust;
	Vector3 oldPosition;
	float clearTimeStart = 0,
		ticsToJump = 0;
	bool clear = false;



	public override void RunState()
	{
		// apply acceleration due to gravity
		blackboard.targetVelocity += Vector3.Cross(blackboard.feet.checkFeet.normal, Vector3.Cross(blackboard.feet.checkFeet.normal, -Physics.gravity)) * Time.fixedDeltaTime;

		//Debug.DrawRay(transform.position, blackboard.targetVelocity);

		// get and set sliding velocity
		if(blackboard.feet.isGrounded)
		{
			blackboard.velocity = blackboard.targetVelocity;
		}

		// move
		blackboard.agent.Move((blackboard.velocity) * Time.fixedDeltaTime);

		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.character.forward = Vector3.Slerp(blackboard.character.forward, blackboard.lookDirection, Time.fixedDeltaTime * 15);

		// clear ledge timer
		if(!clear && !blackboard.feet.isGrounded)
		{
			clear = true;
			clearTimeStart = Time.time;
			// dust
			//feetDust.Stop();
		}

		// check position
		if(Vector3.Distance(oldPosition, transform.root.position) < 0.1f)
		{
			ticsToJump++;
		}
		else
		{
			oldPosition = transform.root.position;
			ticsToJump = 0;
		}
	}



	public override void StartState()
	{
		// reset ledge clear
		clear = false;
		clearTimeStart = Time.time;

		// reset jump tics
		ticsToJump = 0;

		// get slide target velocity
		blackboard.targetVelocity = Vector3.Cross(blackboard.feet.checkFeet.normal, Vector3.Cross(blackboard.feet.checkFeet.normal, -Physics.gravity.normalized)) * blackboard.speed;

		// animate
		//blackboard.anim.ResetTrigger("jump");
		//blackboard.anim.SetTrigger("slide");

		// dust
		//feetDust.Play();

		// sound
		blackboard.charAud.SlideStart();

		// set look y
		blackboard.cameraPivotController.pauseY = false;
	}



	public override void EndState()
	{
		// dust
		//feetDust.Stop();
		// sound
		blackboard.charAud.SlideStop();
	}



	public override State Transition()
	{
		if(ticsToJump > 20)
		{
			return blackboard.jumpState;
		}

		if((blackboard.feet.isGrounded || Time.time < clearTimeStart + 0.1f) && (blackboard.feet.angle < blackboard.maxSlope || blackboard.feet.angleRay < blackboard.maxSlope))
		{
			if(blackboard.feet.isFlat || blackboard.feet.isFlatRay)
			{
				// grounded
				return blackboard.groundedSuperState;
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
