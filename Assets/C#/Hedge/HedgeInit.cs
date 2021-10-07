using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeInit : MonoBehaviour
{
    



    void Awake()
    {
        // set player's starting hub location to the hedge entrance
		HubDataManager.data.startingDoor = "hedge";
		HubDataManager.SaveData();
	}
}
