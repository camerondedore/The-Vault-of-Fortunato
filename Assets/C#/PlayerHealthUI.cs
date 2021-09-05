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
	[SerializeField]
	RectTransform animatedHeart,
		animatedHeartTarget;
	Vector3 animatedHeartStartPosition;
	float animatedHeartLerpValue = 1;
	float oldHitPoints = -1;



	void Start()
	{
		// init animated heart
		animatedHeartStartPosition = animatedHeart.position;
		animatedHeartTarget = hearts[((int) health.hitPoints) - 1].GetComponent<RectTransform>();

		oldHitPoints = health.hitPoints;
		
		ShowOrHideHearts();
	}
	


	public void Update()
	{
		if(oldHitPoints != health.hitPoints)
		{
			oldHitPoints = health.hitPoints;

			ShowOrHideHearts();

			// reset heart animation
			animatedHeartLerpValue = 0;
			animatedHeartTarget = hearts[((int) health.hitPoints) - 1].GetComponent<RectTransform>();
			
		}

		// animate the scale of the hearts
		var index = 0;
		foreach(var h in hearts)
		{
			// animate only displayed hearts
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

		// animate heart
		animatedHeartLerpValue = Mathf.Clamp01(animatedHeartLerpValue + Time.deltaTime * 3);
		animatedHeart.position = Vector3.Lerp(animatedHeartStartPosition, animatedHeartTarget.position, animatedHeartLerpValue);
	}



	public void ShowOrHideHearts()
	{
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
