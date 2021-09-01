using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBrick : MonoBehaviour, IPickup
{
    




	void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
    }



	public void Pickup(Transform player)
	{
		PlayerBricks.AddBrick();
		// brick code here
		Destroy(gameObject);
	}
}
