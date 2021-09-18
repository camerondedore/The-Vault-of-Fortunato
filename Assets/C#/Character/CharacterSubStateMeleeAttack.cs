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
	bool meleeDamage = false,
		chainAttack = false;



	public override void RunState()
	{
		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, (Time.time - startTime) / attackTime);

		// set constant downward velocity
		blackboard.y = 1f;	

		// move to apply gravity
		blackboard.agent.Move((blackboard.velocity + Physics.gravity.normalized * blackboard.y) * Time.deltaTime);

		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.characterMesh.forward = Vector3.Slerp(blackboard.characterMesh.forward, blackboard.lookDirection, Time.deltaTime * blackboard.lookSpeed);

		if(!meleeDamage && Time.time > startTime + meleeTime)
		{
			meleeDamage = true;
			
			// attack
			blackboard.melee.Attack();
		}

		if(blackboard.fire1Disconnector.Trip(blackboard.input.fire1) && Time.time > startTime + attackTime * 0.75f)
		{
			chainAttack = true;
		}
	}



	public override void StartState()
	{
		// reset attack number
		if(endTime + 5 < Time.time)
		{
			attackNumber = 1;
		}
		
		// idle
		blackboard.anim.SetTrigger("Attack");
		blackboard.anim.SetInteger("Attack Number", attackNumber);

		// get start time
		startTime = Time.time;

		// reset melee
		meleeDamage = false;

		// set target velocity
		blackboard.targetVelocity = Vector3.zero;

		// start trail
		blackboard.meleeTrail.emitting = true;
	}



	public override void EndState()
	{
		attackNumber++;

		if(attackNumber > 2)
		{
			attackNumber = 1;
		}

		endTime = Time.time;

		// reset chain
		chainAttack = false;

		// stop trail
		blackboard.meleeTrail.emitting = false;
		blackboard.meleeTrail.Clear();
	}



	public override State Transition()
	{	
		if(startTime + attackTime < Time.time)
		{
			if(!chainAttack)
			{
				// idle
				return blackboard.idleSubState;		
			}

			// attack chain
			this.EndState();
			this.StartState();
		}

		return this;
	}
}
