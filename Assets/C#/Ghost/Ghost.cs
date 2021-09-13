using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    
	[SerializeField]
	Transform target,
		ghostMesh;
	[SerializeField]
	GhostAudio aud;
	Vector3 startPosition,
		endPosition,
		targetPosition;
	Quaternion targetRotation;
	float lastHitTime;



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
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 270 * Time.deltaTime);

		// ping pong mesh
		ghostMesh.localPosition = Vector3.up * Mathf.Sin(Time.time * 3.14f * 0.5f) * 0.2f;
    }



	void OnTriggerEnter(Collider col)
	{
		if(lastHitTime + 1 < Time.time)
		{
			var health = col.transform.root.GetComponent<Health>();
			health.Damage(1);
			lastHitTime = Time.time;
			aud.PlayAttack();
		}
	}
}
