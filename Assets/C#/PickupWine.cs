using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PickupWine : MonoBehaviour, IPickup
{
    
	[SerializeField]
	Rigidbody bottle,
		cork;



	void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
    }



	public void Pickup(Transform player)
	{
		var health = player.GetComponent<Health>();
		if(health.hitPoints >= health.maxHitPoints)
		{
			return;
		}

		// heal
		health.Heal(1);
		
		// fx init
		bottle.transform.parent = null;
		cork.transform.parent = null;
		bottle.gameObject.SetActive(true);
		cork.gameObject.SetActive(true);
		// fx
		cork.AddForce(cork.transform.TransformDirection((Vector3.up + Random.insideUnitSphere * 0.1f) * 5), ForceMode.VelocityChange);
		bottle.AddForce(bottle.transform.TransformDirection((Vector3.up + Random.insideUnitSphere * 0.1f) * -5), ForceMode.VelocityChange);
		cork.transform.localScale *= 1.5f;
		// cleanup
		Destroy(cork.gameObject, 5);
		Destroy(bottle.gameObject, 10);
		
		Destroy(gameObject);
	}
}
