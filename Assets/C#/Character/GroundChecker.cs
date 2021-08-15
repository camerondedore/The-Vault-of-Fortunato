using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
   
    public LayerMask mask;
	public RaycastHit checkFeet,
		checkHead,
		checkFeetRay;
	public float angle, 
		angleRay;
    public bool isGroundedRay,
		isGrounded,
		isFlatRay,
		isFlat,
		headBump;
	float distance,
		rayDistance,
		radius,
		maxAngle;



	void Start()
	{
		var controller = transform.root.GetComponent<CharacterController>();
		
		// get radius
		radius = controller.radius;
		// get distance for sphere cast
		distance = controller.height * 0.5f - radius + controller.skinWidth + 0.02f - controller.center.y;
		// get distance for ray cast
		rayDistance = controller.height * 0.5f + controller.stepOffset + controller.skinWidth + 0.02f - controller.center.y;
		// get angle
		maxAngle = controller.slopeLimit;
	}



	void FixedUpdate()
	{
		// feet check
		Physics.SphereCast(transform.position, radius, Physics.gravity, out checkFeet, distance, mask);
		isGrounded = checkFeet.collider != null;
		angle = Vector3.Angle(Vector3.up, checkFeet.normal);
		isFlat = isGrounded && angle < maxAngle;
		
		// feet ray check
		Physics.Raycast(transform.position, Physics.gravity, out checkFeetRay, rayDistance, mask);
		isGroundedRay = checkFeetRay.collider != null;
		angleRay = Vector3.Angle(Vector3.up, checkFeetRay.normal);
		isFlatRay = isGroundedRay && angleRay < maxAngle;
		//Debug.DrawRay(transform.position, Physics.gravity.normalized * rayDistance);

		// head check
		Physics.SphereCast(transform.position, radius, -Physics.gravity, out checkHead, distance, mask);
		headBump = checkHead.collider != null && Vector3.Angle(Vector3.up, checkHead.normal) > 90;
	}



	public GameObject GetGround()
	{
		if(isGrounded)
		{
			return checkFeet.collider.gameObject;
		}

		if(isGroundedRay)
		{
			return checkFeetRay.collider.gameObject;
		}

		return null;
	}
}
