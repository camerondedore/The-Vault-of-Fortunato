using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSubStateMeleeAttack2 : CharacterState
{

	[SerializeField]
	GameObject display;
	[SerializeField]
	float attackTime = 0.4f,
		meleeTime = 0.2f;
	float startTime;
	bool queueAttack = false,
		meleeDamage = false;



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
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeet.normal).normalized * 
				blackboard.speed * 0.5f;
		}
		else if(blackboard.feet.isFlatRay)
		{
			// ray grounded 
			blackboard.targetVelocity = Vector3.ProjectOnPlane(moveDir, blackboard.feet.checkFeetRay.normal).normalized * blackboard.speed;
		}

		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, (Time.time - startTime) / attackTime);

		// set constant downward velocity
		blackboard.y = 1f;	

		// move to apply gravity
		blackboard.agent.Move((blackboard.velocity + Physics.gravity.normalized * blackboard.y) * Time.fixedDeltaTime);

		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.character.forward = Vector3.Slerp(blackboard.character.forward, blackboard.lookDirection, Time.fixedDeltaTime * blackboard.lookSpeed);

		// get input
		queueAttack = blackboard.fire1Disconnector.Trip(blackboard.input.fire2);

		if(!meleeDamage && Time.time > startTime + meleeTime)
		{
			meleeDamage = true;
			
			// attack
			blackboard.melee.Attack();

			display.SetActive(true);
		}		

		// tmp hide display
		if(Time.time > startTime + meleeTime + 0.1f)
		{
			display.SetActive(false);
		}
	}



	public override void StartState()
	{
		// idle
		//blackboard.anim.SetFloat("timeScale", 1);

		// get start time
		startTime = Time.time;

		// reset melee
		meleeDamage = false;
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		if(startTime + attackTime < Time.time)
		{
			if(queueAttack)
			{
				// attack 3
				return blackboard.meleeAttack3SubState;
			}

			// idle
			return blackboard.idleSubState;		
		}	

		return this;
	}
}
