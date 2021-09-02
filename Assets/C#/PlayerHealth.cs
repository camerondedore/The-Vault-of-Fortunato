using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    
	[SerializeField]
	CharacterBlackboard blackboard;



	public override void Die()
	{
		blackboard.machine.SetState(blackboard.dieState);
	}
}
