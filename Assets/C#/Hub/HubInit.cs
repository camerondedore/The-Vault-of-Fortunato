using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubInit : MonoBehaviour
{
    




    void Awake()
    {
        // set player's starting inn location to the front door
		InnDataManager.data.startAtDoor = true;
		InnDataManager.SaveData();
    }    
}
