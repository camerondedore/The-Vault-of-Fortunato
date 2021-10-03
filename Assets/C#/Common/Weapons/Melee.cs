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
	[SerializeField]
	AudioSourceController aud;
	[SerializeField]
	AudioClip hitSound;



	void Start()
	{

	}



	public void Attack()
	{
		// get colliders
		var hitColliders = Physics.OverlapSphere(transform.position, radius, mask, QueryTriggerInteraction.Ignore);
		//Debug.DrawRay(transform.position, transform.forward * radius);

		var hitReal = false;
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

			// queue audio
			hitReal = true;
		}

		// audio
		if(hitReal)
		{
			aud.PlayOneShot(hitSound);
		}
	}
}
