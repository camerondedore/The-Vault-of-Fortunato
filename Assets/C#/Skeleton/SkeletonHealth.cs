using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : Health
{
    
	[SerializeField]
	SkeletonBlackboard blackboard;



	void Start()
	{

	}



	public override void Damage(float dmg)
	{
		hitPoints = Mathf.Clamp(hitPoints - dmg, 0, maxHitPoints);

		if(hitPoints == 0 && !base.dead)
		{
			base.dead = true;
			Die();
		}
		else if(!dead)
		{
			// hurt
			blackboard.machine.SetState(blackboard.hurtState);
		}
	}



	public override void Heal(float hp)
	{
		hitPoints = Mathf.Clamp(hitPoints + hp, 0, maxHitPoints);
	}



	public override void Die()
	{
		blackboard.machine.SetState(blackboard.dieState);
	}
}
