﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSubStateIdle : CharacterState
{

	[SerializeField]
	float idleRepsonseSpeed = 8;



	public override void RunState()
	{
		// smooth velocity to target velocity
		blackboard.velocity = Vector3.Lerp(blackboard.velocity, blackboard.targetVelocity, Time.fixedDeltaTime * idleRepsonseSpeed);

		// move to apply gravity
		blackboard.agent.Move((blackboard.velocity + Physics.gravity.normalized * blackboard.y) * Time.fixedDeltaTime);

		// look
		if(blackboard.velocity.sqrMagnitude > 0.1f)
		{
			blackboard.lookDirection = blackboard.velocity;
			blackboard.lookDirection.y = 0;
		}
		blackboard.character.forward = Vector3.Slerp(blackboard.character.forward, blackboard.lookDirection, Time.fixedDeltaTime * blackboard.lookSpeed);
	}



	public override void StartState()
	{
		// idle
		//blackboard.anim.SetFloat("timeScale", 1);

		// reset velocity
		blackboard.targetVelocity = Vector3.zero;
		blackboard.y = 1;

		// enable hands
		//blackboard.hands.enabled = true;
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{	
		if(blackboard.fire1Disconnector.Trip(blackboard.input.fire1))
		{
			// attack 1
			return blackboard.meleeAttackSubState;
		}		

		if(blackboard.input.moveDirection.sqrMagnitude > 0.1f)
		{
			// move
			return blackboard.moveSubState;
		}

		return this;
	}
}