using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateHome : MenuState
{
    
	



	public override void RunState()
	{

	}



	public override void StartState()
	{
		blackboard.home.SetActive(true);
	}



	public override void EndState()
	{
		blackboard.home.SetActive(false);
	}



	public override State Transition()
	{
		return this;
	}
}
