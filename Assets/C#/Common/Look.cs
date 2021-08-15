using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{

	public static float lookSensitivity = 7;
	[SerializeField] 
	Transform head = null;
	[SerializeField]
	float lookLimit = 30;
	Vector2 lookAngles,
		lookRawChange;



	void Awake()
	{
		lookAngles = new Vector2(0, transform.localEulerAngles.y);
	}



	void FixedUpdate()
	{
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
