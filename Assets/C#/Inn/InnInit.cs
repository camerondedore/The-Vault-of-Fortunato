using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnInit : MonoBehaviour
{

	[SerializeField]
	Transform player,
		frontDoorStart,
		mainCamera,
		frontDoorCameraStart;



    void Awake()
    {
        InnDataManager.LoadData();
		if(InnDataManager.data.startAtDoor == true)
		{
			// set player's starting location to the front door
			player.position = frontDoorStart.position;
			// set camera's starting location to the front door
			mainCamera.position = frontDoorCameraStart.position;
		}
    }

}
