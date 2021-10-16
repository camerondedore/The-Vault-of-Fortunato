using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPauseClear : MonoBehaviour
{
    
	



    void Start()
    {
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
        Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
    }
}
