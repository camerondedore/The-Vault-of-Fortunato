using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStatePlay : MenuState
{
    
	float startTime;
	bool playing = false;



	public override void RunState()
	{
		if(startTime + 1 < Time.time && !playing)
		{
			playing = true;

			// set player's starting inn location to the cellar
			InnDataManager.data.startAtDoor = false;
			InnDataManager.SaveData();

			SceneLoader.LoadLevel("Inn");
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
