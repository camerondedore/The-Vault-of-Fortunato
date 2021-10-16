using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateControls : MenuState
{
    
	



	public override void RunState()
	{

	}



	public override void StartState()
	{
		blackboard.controls.SetActive(true);
	}



	public override void EndState()
	{
		blackboard.controls.SetActive(false);
	}



	public override State Transition()
	{
		return this;
	}
}
