using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Pause : MonoBehaviour
{

	Disconnector disconnector = new Disconnector();



	void Start()
	{
		PauseSet(false);
	}



	void Update()
	{
		if (disconnector.Trip(PlayerInput.pause))
		{
			PauseToggle();
		}
	}



	void PauseToggle()
	{		
		// unpaused? ? pause : unpause
		Time.timeScale = Time.timeScale > 0 ? 0 : 1;
		
		if(Time.timeScale > 0)
		{
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
		else
		{
			Time.fixedDeltaTime = 0.02f;
		}
		
		Cursor.visible = !Cursor.visible ? true : false;
		Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;		
	}



	public void PauseSet(bool paused)
	{
		// unpaused? ? pause : unpause
		Time.timeScale = paused ? 0 : 1;

		if (Time.timeScale > 0)
		{
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
		else
		{
			Time.fixedDeltaTime = 0.02f;
		}
		
		Cursor.visible = paused ? true : false;
		Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
	}
}
