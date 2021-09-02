using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBricksUI : MonoBehaviour
{
    
	[SerializeField]
	PlayerBricks bricks;
	[SerializeField]
	Text brickCounter;
	int oldBrickCount = -1;



	void Update()
	{
		// update UI
		if(oldBrickCount != bricks.bricksCollected)
		{
			oldBrickCount = bricks.bricksCollected;
			brickCounter.text = $"{bricks.bricksCollected}/{bricks.totalBricks}";
		}
	}
}
