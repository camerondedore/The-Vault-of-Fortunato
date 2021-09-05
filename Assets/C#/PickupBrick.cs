using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PickupBrick : MonoBehaviour, IPickup
{
    
	[SerializeField]
	ParticleSystem brickFX;



	void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
    }



	public void Pickup(Transform player)
	{
		player.GetComponent<PlayerBricks>().AddBrick();
		// fx
		brickFX.transform.parent = null;
		brickFX.Play();
		// brick code here
		Destroy(gameObject);
	}
}
