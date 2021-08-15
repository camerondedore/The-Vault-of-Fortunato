using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
   
	Vector3 startPosition;



	void Start()
	{
		startPosition = transform.position;
	}



	void FixedUpdate()
	{
		transform.position = Vector3.Lerp(startPosition, startPosition + transform.forward * -4, Mathf.PingPong(Time.time/3, 1));
	}
}
