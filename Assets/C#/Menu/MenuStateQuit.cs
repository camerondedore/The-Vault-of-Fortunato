using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateQuit : MenuState
{
    
	float startTime;
	bool quiting = false;



	public override void RunState()
	{
		if(startTime + 1 < Time.time && !quiting)
		{
			quiting = true;

			Application.Quit();
		}
	}



	public override void StartState()
	{
		startTime = Time.time;
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		return this;
	}
}
