using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBricks : MonoBehaviour
{
    
	public int bricksCollected = 0;
	[HideInInspector]
	public int totalBricks = 18;



	public void AddBrick()
	{
		bricksCollected++;
	}
}
