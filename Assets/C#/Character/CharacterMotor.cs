using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    
	public CharacterController agent;
	public Transform characterMesh;
	public Vector3 targetVelocity,
		velocity,
		lookDirection;
	public float y,
		reponseSpeed,
		lookSpeed = 10,
		speed = 3.5f;



    void FixedUpdate()
    {
		// smooth velocity to target velocity
		velocity = Vector3.Lerp(velocity, targetVelocity, Time.fixedDeltaTime * reponseSpeed);

		// clamp y
		y = Mathf.Clamp(y, -10, 100);

		// move
		agent.Move((velocity + Physics.gravity.normalized * y) * Time.fixedDeltaTime);
		
		// look
		if(velocity.sqrMagnitude > 0.1f)
		{
			lookDirection = velocity;
			lookDirection.y = 0;
		}
		characterMesh.forward = Vector3.Slerp(characterMesh.forward, lookDirection, Time.fixedDeltaTime * lookSpeed);
    }
}
