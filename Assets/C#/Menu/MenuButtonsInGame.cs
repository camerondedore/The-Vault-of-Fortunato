using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsInGame : MonoBehaviour
{
    
	[SerializeField]
	Pause pause;



	public void ReturnToMenu()
	{
		SceneLoader.LoadLevel("Menu");
	}



	public void ResumeGame()
	{
		pause.PauseSet(false);
	}
}
