using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHurt : CharacterState
{
   
	[SerializeField]
	RendererFlasher characterFlasher;
	float startTime = 0;



	public override void RunState()
	{
		// move
		blackboard.agent.Move((-blackboard.lookDirection.normalized) * Time.deltaTime);
	}



	public override void StartState()
	{
		// animate
		blackboard.anim.SetTrigger("Hurt");

		startTime = Time.time;

		// flash
		characterFlasher.startTime = Time.time;

		// sound
		blackboard.charAud.PlayLand();

		// fx
		blackboard.blood.Play();
	}



	public override void EndState()
	{
		
	}



	public override State Transition()
	{
		if(startTime + 0.3f < Time.time)
		{
			// grounded
			return blackboard.groundedSuperState;
		}

		return this;
	}
}
