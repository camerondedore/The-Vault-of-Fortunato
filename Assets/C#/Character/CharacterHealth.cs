using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : Health
{
    
	public bool vulnerable = true;
	[SerializeField]
	CharacterBlackboard blackboard;



	void Start()
	{
		// init from saved data
		hitPoints = PlayerDataManager.data.hitPoints;
	}



	public override void Damage(float dmg)
	{
		if(!vulnerable)
		{
			return;
		}

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

			// save data
			PlayerDataManager.data.hitPoints = hitPoints;
			PlayerDataManager.SaveData();
		}
	}



	public override void Heal(float hp)
	{
		hitPoints = Mathf.Clamp(hitPoints + hp, 0, maxHitPoints);

		// save data
		PlayerDataManager.data.hitPoints = hitPoints;
		PlayerDataManager.SaveData();
	}



	public override void Die()
	{
		// reset health for restarting
		PlayerDataManager.data.hitPoints = maxHitPoints;
		PlayerDataManager.SaveData();

		blackboard.machine.SetState(blackboard.dieState);
	}
}
