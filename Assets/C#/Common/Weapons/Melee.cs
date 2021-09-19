using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    
	[SerializeField]
	LayerMask mask;
	[SerializeField]
	float radius = 0.75f,
		damage = 1;



	void Start()
	{

	}



	public void Attack()
	{
		// get colliders
		var hitColliders = Physics.OverlapSphere(transform.position, radius, mask, QueryTriggerInteraction.Ignore);
		//Debug.DrawRay(transform.position, transform.forward * radius);

		foreach(var c in hitColliders)
		{
			//Debug.Log(c.transform.root.name);
			
			if(c.transform.root == transform.root)
			{
				// don't hit self
				continue;
			}
			
			// damage
			var hitbox = c.GetComponent<IDamageable>();
			if(hitbox != null)
			{
				hitbox.Hit(damage);
			}
		}
	}
}
