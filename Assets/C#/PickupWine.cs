using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PickupWine : MonoBehaviour, IPickup
{
    




	void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
    }



	public void Pickup(Transform player)
	{
		player.GetComponent<Health>().Heal(1);
		// heal code here
		Destroy(gameObject);
	}
}
