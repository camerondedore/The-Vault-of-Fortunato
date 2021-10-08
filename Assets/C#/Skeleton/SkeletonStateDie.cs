using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateDie : SkeletonState
{

	[SerializeField]
	float destroyTime = 5;
	float startTime;



	public override void RunState()
	{
		if(Time.time > startTime + destroyTime)
		{
			// destroy
			Destroy(transform.root.gameObject);
		}
	}



	public override void StartState()
	{
		// clear velocity
		blackboard.agent.isStopped = true;

		startTime = Time.time;

		// turn off all colliders
		var colliders = transform.root.GetComponentsInChildren<Collider>();
		foreach(var c in colliders)
		{
			if(c.GetType() != typeof(CharacterController))
			{
				c.enabled = false;
			}
		}

		// die audio
		blackboard.aud.PlayDie();

		blackboard.mesh.SetActive(false);

		// gib
		blackboard.gibber.InitiateGibs();

		// fx
		blackboard.boneSplinters.Play();

		// clean
		Destroy(transform.root.gameObject, 10);
	}



	public override void EndState()
	{

	}



	public override State Transition()
	{
		return this;
	}
}
