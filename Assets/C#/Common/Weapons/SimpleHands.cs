using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHands : MonoBehaviour
{
    
	Weapon weapon;



    void Start()
    {
		
    }



	void Update()
	{
		if(Time.timeScale == 0 || Time.deltaTime == 0)
		{
			return;
		}

		// get weapon
		weapon = GetComponentInChildren<Weapon>();

		// pull trigger
		var willFire = weapon.PullTrigger(PlayerInput.fire1);

		// fire
		if(willFire)
		{
			// animation 
			// code here ...
		}
	}
}
