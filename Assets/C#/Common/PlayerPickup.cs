using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    




	void OnTriggerEnter(Collider co)
	{
		var pickup = co.transform.GetComponent<IPickup>();
		pickup.Pickup(transform.root);
	}
}
