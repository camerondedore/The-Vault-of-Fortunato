using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManorInit : MonoBehaviour
{
    



    void Awake()
    {
        // set player's starting hub location to the manor entrance
		HubDataManager.data.startingDoor = "manor";
		HubDataManager.SaveData();
	}
}