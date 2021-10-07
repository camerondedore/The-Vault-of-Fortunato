using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemeteryInit : MonoBehaviour
{




    void Awake()
    {
        // set player's starting hub location to the cemetery entrance
		HubDataManager.data.startingDoor = "cemetery";
		HubDataManager.SaveData();
	}
}
