using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBricks : MonoBehaviour
{
    
	public static int bricksCollected = 0;
	[SerializeField]
	Text brickCounter;
	int oldBrickCount = 0;



	void Update()
	{
		if(oldBrickCount != bricksCollected)
		{
			oldBrickCount = bricksCollected;
			brickCounter.text = $"{bricksCollected}/18";
		}
	}



	public static void AddBrick()
	{
		bricksCollected++;
	}
}
