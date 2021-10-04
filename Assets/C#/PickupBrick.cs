using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SelectionBase]
public class PickupBrick : MonoBehaviour, IPickup
{
    
	[SerializeField]
	ParticleSystem brickFX;
	string brickId = "scene-location";



	void Start()
	{
		// generate id using scene and position
		brickId = $"{SceneManager.GetActiveScene().name.ToLower()}-{transform.position}";

		// check if already collected
		if (PlayerDataManager.data.brickIds.Contains(brickId))
		{
			Destroy(gameObject);
		}
	}



	void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
    }



	public void Pickup(Transform player)
	{
		player.GetComponent<PlayerBricks>().AddBrick(brickId);
		// fx
		brickFX.transform.parent = null;
		brickFX.Play();
		// brick code here
		Destroy(gameObject);
	}
}
