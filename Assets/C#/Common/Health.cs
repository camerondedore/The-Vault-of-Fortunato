using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

	public float hitPoints = 100;
	bool dead = false;



	public void Damage(float dmg)
	{
		hitPoints = Mathf.Clamp(hitPoints - dmg, 0, Mathf.Infinity);

		if(hitPoints == 0 && !dead)
		{
			dead = true;
			Die();
		}
	}



	public virtual void Die()
	{
		// death here
		Destroy(gameObject);
	}
}
