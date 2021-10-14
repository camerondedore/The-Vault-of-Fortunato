using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    
	



	public void Play()
	{
		// set player's starting inn location to the cellar
		InnDataManager.data.startAtDoor = false;
		InnDataManager.SaveData();

		SceneLoader.LoadLevel("Inn");
	}



	public void Quit()
	{
		Application.Quit();
	}



	public void ClearSavedData()
	{
		// clear bricks and health from save file
		PlayerDataManager.data = new PlayerDataManager.PlayerData();
		PlayerDataManager.SaveData();
	}
}
