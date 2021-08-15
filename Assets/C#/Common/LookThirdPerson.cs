using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookThirdPerson : MonoBehaviour
{

	public static float lookSensitivity = 7;
	public bool pauseY = false;
	[SerializeField] 
	Transform head = null;
	[SerializeField]
	float lookLimit = 30;
	Vector2 lookAngles,
		lookRawChange;
	float targetY,
		offsetY,
		ySpeed = 5,
		lastY;



	void Awake()
	{
		lookAngles = new Vector2(0, transform.localEulerAngles.y);
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

		// apply look
		if(lookRawChange.sqrMagnitude > 0)
		{
			var change = lookRawChange * lookSensitivity * Time.fixedDeltaTime;
			lookAngles += change;
			lookAngles.x = Mathf.Clamp(lookAngles.x, -lookLimit, lookLimit);

			var headLocalEuler = head.localEulerAngles;
			headLocalEuler.x = lookAngles.x;

			var bodyLocalEuler = transform.localEulerAngles;
			bodyLocalEuler.y = lookAngles.y;

			head.localRotation = Quaternion.Euler(headLocalEuler);
			transform.localRotation = Quaternion.Euler(bodyLocalEuler);
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



	public void Update()
	{
		var change = -PlayerInput.look;
		change.y *= -1;
		lookRawChange = change;
	}



	public void LookAt(Vector3 direction)
	{
		var headDirection = Vector3.ProjectOnPlane(direction, head.right);
		var bodyDirection = Vector3.ProjectOnPlane(direction, transform.up);

		head.forward = headDirection;
		transform.forward = bodyDirection;
	}
}
