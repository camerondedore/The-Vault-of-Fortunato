using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

	public float hitPoints = 100,
		maxHitPoints = 100;
	bool dead = false;



	public void Damage(float dmg)
	{
		hitPoints = Mathf.Clamp(hitPoints - dmg, 0, maxHitPoints);

		if(hitPoints == 0 && !dead)
		{
			dead = true;
			Die();
		}
	}



	public void Heal(float hp)
	{
		hitPoints = Mathf.Clamp(hitPoints + hp, 0, maxHitPoints);
	}



	public virtual void Die()
	{
		// death here
		Destroy(gameObject);
	}
}
