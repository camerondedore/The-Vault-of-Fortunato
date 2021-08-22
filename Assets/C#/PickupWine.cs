using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PickupWine : MonoBehaviour, IPickup
{
    




	void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 90 * Time.fixedDeltaTime, Space.World);
    }



	public void Pickup(Transform player)
	{
		// heal code here
		Destroy(gameObject);
	}
}
