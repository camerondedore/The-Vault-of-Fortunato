using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HubInit : MonoBehaviour
{
    
	[SerializeField]
	Transform player,
		mainCamera,
		innStart,
		cemeteryStart,
		manorStart,
		hedgeStart,
		innCameraStart,
		cemeteryCameraStart,
		manorCameraStart,
		hedgeCameraStart;
	[SerializeField]
	GameObject cemeterySkeleton,
		manorSkeleton,
		hedgeSkeleton;



    void Awake()
    {
        // set player's starting inn location to the front door
		InnDataManager.data.startAtDoor = true;
		InnDataManager.SaveData();


		HubDataManager.LoadData();
		if(HubDataManager.data.startingDoor == "inn")
		{
			// set player's starting location to the inn
			player.position = innStart.position;
			player.forward = innStart.forward;
			// set camera's starting location to the inn
			mainCamera.position = innCameraStart.position;
		}
		else if(HubDataManager.data.startingDoor == "cemetery")
		{
			// set player's starting location to the cemetery
			player.position = cemeteryStart.position;
			player.forward = cemeteryStart.forward;
			// set camera's starting location to the cemetery
			mainCamera.position = cemeteryCameraStart.position;
		}
		else if(HubDataManager.data.startingDoor == "manor")
		{
			// set player's starting location to the manor
			player.position = manorStart.position;
			player.forward = manorStart.forward;
			// set camera's starting location to the manor
			mainCamera.position = manorCameraStart.position;
		}
		else if(HubDataManager.data.startingDoor == "hedge")
		{
			// set player's starting location to the hedge maze
			player.position = hedgeStart.position;
			player.forward = hedgeStart.forward;
			// set camera's starting location to the hedge maze
			mainCamera.position = hedgeCameraStart.position;
		}

		// spawn skeleton at cemetery if enough bricks are collected
		if(PlayerDataManager.data.brickIds.Where(id => id.Contains("cemetery")).Count() == 5 &&
			HubDataManager.data.startingDoor != "cemetery")
		{
			cemeterySkeleton.SetActive(true);
		}

		// spawn skeleton at manor if enough bricks are collected
		if(PlayerDataManager.data.brickIds.Where(id => id.Contains("manor")).Count() == 5 &&
			HubDataManager.data.startingDoor != "manor")
		{
			manorSkeleton.SetActive(true);
		}

		// spawn skeleton at hedge if enough bricks are collected
		if(PlayerDataManager.data.brickIds.Where(id => id.Contains("hedge")).Count() == 5 &&
			HubDataManager.data.startingDoor != "hedge")
		{
			hedgeSkeleton.SetActive(true);
		}
    }    
}
