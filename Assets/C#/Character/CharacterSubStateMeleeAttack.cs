using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSubStateMeleeAttack : CharacterState
{

	[SerializeField]
	float attackTime = 0.4f,
		meleeTime = 0.2f;
	float startTime,
		endTime;
	int attackNumber = 1;
	bool meleeDamage = false;



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
		blackboard.characterMesh.forward = Vector3.Slerp(blackboard.characterMesh.forward, blackboard.lookDirection, Time.fixedDeltaTime * blackboard.lookSpeed * 0.5f);

		if(!meleeDamage && Time.time > startTime + meleeTime)
		{
			meleeDamage = true;
			
			// attack
			blackboard.melee.Attack();
		}
	}



	public override void StartState()
	{
		// idle
		blackboard.anim.SetTrigger("Attack");
		blackboard.anim.SetInteger("Attack Number", attackNumber);

		// get start time
		startTime = Time.time;

		// reset melee
		meleeDamage = false;

		// reset attack number
		if(endTime + 5 < Time.time)
		{
			attackNumber = 1;
		}
	}



	public override void EndState()
	{
		attackNumber++;

		if(attackNumber > 3)
		{
			attackNumber = 1;
		}

		endTime = Time.time;
	}



	public override State Transition()
	{	
		if(startTime + attackTime < Time.time)
		{
			// idle
			return blackboard.idleSubState;		
		}	

		return this;
	}
}
