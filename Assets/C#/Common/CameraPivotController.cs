using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotController : MonoBehaviour
{

	public bool pauseY = false;
	float targetY,
		offsetY,
		ySpeed = 5,
		lastY;



	void Awake()
	{
		targetY = transform.position.y;
		offsetY = transform.localPosition.y;
	}



	void LateUpdate()
	{
		// check for pause
		if(Time.timeScale <= 0)
		{
			return;
		}

		// get y
		if(!pauseY)
		{
			if(targetY < transform.parent.position.y + offsetY)
			{
				// going up
				targetY = Mathf.Lerp(targetY, transform.parent.position.y + offsetY, Time.fixedDeltaTime * ySpeed);
			}
			else
			{
				// going down
				targetY = transform.parent.position.y + offsetY;
			}

			// store for use when paused
			lastY = transform.parent.position.y + offsetY;
		}
		else
		{
			// paused, coninue to last y
			if(targetY < lastY)
			{
				// going up
				targetY = Mathf.Lerp(targetY, lastY, Time.fixedDeltaTime * ySpeed);
			}
		}

		// smooth to y
		var targetPosition = transform.parent.position;
		targetPosition.y = targetY;
		transform.position = targetPosition;
	}
}
