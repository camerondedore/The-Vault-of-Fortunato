using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTracker : MonoBehaviour, IInitializeIntoBlackboard
{

	[HideInInspector]
	public Vector3 velocity,
		fixedDeltaTimeVelocity,
		localVelocity;
	[HideInInspector]
	public float speed;
	Vector3 oldPosition;



	public string GetBlackboardKey()
	{
		return "VelocityTracker";
	}



	void Start()
	{
		oldPosition = transform.position;
	}



	void FixedUpdate()
    {
		if (Time.timeScale == 0 || Time.deltaTime == 0)
		{
			return;
		}

		fixedDeltaTimeVelocity = transform.position - oldPosition;
		velocity = fixedDeltaTimeVelocity / Time.fixedDeltaTime;
		localVelocity = transform.InverseTransformDirection(velocity);
		speed = velocity.magnitude;
		oldPosition = transform.position;
    }
}
