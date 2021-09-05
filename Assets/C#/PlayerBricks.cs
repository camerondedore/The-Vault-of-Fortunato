using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBricks : MonoBehaviour
{
    
	public int bricksCollected = 0;
	[HideInInspector]
	public int totalBricks = 18;



	void Start()
	{
		// init from saved data
		bricksCollected = PlayerDataManager.data.bricks;
	}



	public void AddBrick()
	{
		bricksCollected++;

		// save data
		PlayerDataManager.data.bricks = bricksCollected;
		PlayerDataManager.SaveData();
	}
}
