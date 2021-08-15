using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : State
{

	[HideInInspector]
	public CharacterBlackboard blackboard;



	protected void Awake()
	{
		blackboard = GetComponent<CharacterBlackboard>();
	}
}
