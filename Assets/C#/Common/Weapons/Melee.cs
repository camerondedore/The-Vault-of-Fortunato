using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    
	[SerializeField]
	LayerMask mask;
	[SerializeField]
	Vector3 boxHalves;
	[SerializeField]
	float damage = 1;



	void Start()
	{

	}



	public void Attack()
	{
		// get colliders
		var hitColliders = Physics.OverlapBox(transform.position, boxHalves, transform.rotation, mask, QueryTriggerInteraction.Ignore);

		foreach(var c in hitColliders)
		{
			//Debug.Log(c.name);
			//Debug.DrawRay(transform.position, transform.forward);
			
			// damage
			var hitbox = c.GetComponent<IDamageable>();
			if(hitbox != null)
			{
				hitbox.Hit(damage);
			}
		}
	}
}
