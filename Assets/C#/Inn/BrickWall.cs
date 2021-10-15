using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
	
	[SerializeField]
	GameObject[] bricks;
	[SerializeField]
	GameObject grout;
	int oldBrickCount = -1;



    void Start()
    {
        UpdateBrickWall();
		oldBrickCount = PlayerDataManager.data.brickIds.Count;
    }

    

    void Update()
    {
        if(oldBrickCount != PlayerDataManager.data.brickIds.Count)
		{
			UpdateBrickWall();
			oldBrickCount = PlayerDataManager.data.brickIds.Count;
		}
    }



	void UpdateBrickWall()
	{
		int i = 0;
		while(i < bricks.Length)
		{
			if(i < PlayerDataManager.data.brickIds.Count)
			{
				// show brick
				bricks[i].SetActive(true);
			}
			else
			{
				// hide brick
				bricks[i].SetActive(false);
			}

			i++;
		}

		if(bricks.Length == PlayerDataManager.data.brickIds.Count)
		{
			// show grout
			grout.SetActive(true);
		}
		else
		{
			// hide grout
			grout.SetActive(false);
		}
	}
}
