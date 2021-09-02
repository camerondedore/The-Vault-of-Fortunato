using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    
	[SerializeField]
	Health health;
	[SerializeField]
	GameObject[] hearts;
	float oldHitPoints = -1;
	


	public void Update()
	{
		// update UI
		if(oldHitPoints != health.hitPoints)
		{
			oldHitPoints = health.hitPoints;
			
			var index = 0;
			foreach(var h in hearts)
			{
				if(index <= oldHitPoints - 1)
				{
					// show heart
					h.SetActive(true);
				}
				else
				{
					// hide heart
					h.SetActive(false);
				}

				index++;
			}
		}
	}
}
