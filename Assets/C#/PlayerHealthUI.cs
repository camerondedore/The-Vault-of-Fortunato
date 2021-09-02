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
		ShowOrHideHearts();

		var index = 0;
		foreach(var h in hearts)
		{
			if(index <= health.hitPoints - 1)
			{
				// animate heart
				var speed = health.hitPoints == 3 ? 0.1f : health.hitPoints == 2 ? 0.25f : 1f;
				var phase = index / health.maxHitPoints;
				var scale = 1 + Mathf.PingPong(Time.time * speed + phase, 0.25f);
				h.transform.localScale = Vector3.one * scale;
			}
			else
			{
				break;
			}

			index++;
		}
	}



	public void ShowOrHideHearts()
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
