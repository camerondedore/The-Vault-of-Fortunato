using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHurt : CharacterState
{
   
	float startTime = 0;



	public override void RunState()
	{
		
	}



	public override void StartState()
	{
		// animate
		blackboard.anim.SetTrigger("Hurt");

		startTime = Time.time;

		// sound
		//blackboard.charAud.PlayLand();
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		if(startTime + 0.1f < Time.time)
		{
			// grounded
			return blackboard.groundedSuperState;
		}

		return this;
	}
}
