using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemeteryInit : MonoBehaviour
{




    void Awake()
    {
        // set player's starting inn location to the front door
		HubDataManager.data.startingDoor = "cemetery";
		HubDataManager.SaveData();
	}
}
