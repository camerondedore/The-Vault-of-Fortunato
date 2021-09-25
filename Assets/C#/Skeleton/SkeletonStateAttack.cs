using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateAttack : SkeletonState
{

	[SerializeField]
	float attackTime = 0.4f,
		meleeTime = 0.2f,
		damageRange = 1,
		damageAngle = 90;
	float startTime;
	int attackNumber = 1;
	bool meleeDamage = false;



	public override void RunState()
	{
		if(!meleeDamage && Time.time > startTime + meleeTime)
		{
			meleeDamage = true;
			
			// attack
			// check distance
			var close = Vector3.Distance(blackboard.player.position, transform.root.position) < damageRange;
			// check angle
			var front = Vector3.Angle(blackboard.player.position - transform.root.position, transform.root.forward) < damageAngle;
			if(close && front)
			{
				// damage
				blackboard.player.GetComponent<Health>().Damage(1);
			}
		}
	}



	public override void StartState()
	{	
		// anim
		blackboard.anim.SetTrigger("Attack");
		blackboard.anim.SetInteger("Attack Number", attackNumber);

		// get start time
		startTime = Time.time;

		// reset melee
		meleeDamage = false;

		// start trail
		blackboard.meleeTrail.emitting = true;

		// aim
		var lookDirection = blackboard.player.position - transform.root.position;
		lookDirection.y = 0;
		transform.root.forward = lookDirection;
	}



	public override void EndState()
	{
		attackNumber++;

		if(attackNumber > 2)
		{
			attackNumber = 1;
		}


		// stop trail
		blackboard.meleeTrail.emitting = false;
		blackboard.meleeTrail.Clear();
	}



	public override State Transition()
	{	
		if(startTime + attackTime < Time.time)
		{
			// seek
			return blackboard.seekState;		
		}

		return this;
	}
}
