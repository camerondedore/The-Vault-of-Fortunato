using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{

	[HideInInspector]
	public MenuBlackboard blackboard;



	protected void Awake()
	{
		blackboard = GetComponent<MenuBlackboard>();
	}
}
