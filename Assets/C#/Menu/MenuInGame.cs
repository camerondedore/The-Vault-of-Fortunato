using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    
	[SerializeField]
	GameObject inGameMenu;

    
	
    void Update()
    {
        if(!inGameMenu.activeInHierarchy && Time.timeScale <= 0)
		{
			// show menu
			inGameMenu.SetActive(true);
		}

		if(inGameMenu.activeInHierarchy && Time.timeScale > 0)
		{
			// hide menu
			inGameMenu.SetActive(false);
		}
    }
}
