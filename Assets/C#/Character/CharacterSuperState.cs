using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSuperState : SuperState
{
    
	[HideInInspector]
	public CharacterBlackboard blackboard;



	protected void Awake()
	{
		blackboard = GetComponent<CharacterBlackboard>();
	}
}
