using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    
	[SerializeField]
	Transform target,
		ghostMesh;
	Vector3 startPosition,
		endPosition,
		targetPosition;
	Quaternion targetRotation;



    void Start()
    {
        startPosition = transform.position;
		endPosition = target.position;
		targetPosition = endPosition;
		targetRotation = Quaternion.LookRotation(endPosition - startPosition);
		Destroy(target.gameObject);
    }

    
	
    void FixedUpdate()
    {
		// check ghost position
		if(transform.position == targetPosition)
		{	
			if(targetPosition == endPosition)
			{
				targetPosition = startPosition;
				targetRotation = Quaternion.LookRotation(startPosition - endPosition);
			}
			else
			{
				targetPosition = endPosition;
				targetRotation = Quaternion.LookRotation(endPosition - startPosition);
			}
		}

		// move and rotate
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180 * Time.deltaTime);

		// ping pong mesh
		ghostMesh.localPosition = Vector3.up * Mathf.Sin(Time.time * 3.14f * 0.5f) * 0.2f;
    }
}
