using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundCheckerNav : MonoBehaviour
{
   
    public LayerMask mask;
	public RaycastHit checkFeet,
		checkHead,
		checkFeetRay;
	public float angle, 
		angleRay;
    public bool isGroundedRay,
		isGrounded,
		headBump;
	float distance,
		rayDistance,
		radius,
		maxAngle;



	void Start()
	{
		var controller = transform.root.GetComponent<NavMeshAgent>();
		
		// get distance for sphere cast
		distance = controller.height - controller.baseOffset;
		// get distance for ray cast
		rayDistance = controller.height * 0.5f + 0.02f - controller.baseOffset;
	}



	void FixedUpdate()
	{
		// feet check
		Physics.SphereCast(transform.position, radius, Physics.gravity, out checkFeet, distance, mask);
		isGrounded = checkFeet.collider != null;
		
		// feet ray check
		Physics.Raycast(transform.position, Physics.gravity, out checkFeetRay, rayDistance, mask);
		isGroundedRay = checkFeetRay.collider != null;
		//Debug.DrawRay(transform.position, Physics.gravity.normalized * rayDistance);
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
