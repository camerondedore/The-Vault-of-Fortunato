using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGibs : MonoBehaviour
{
    
	[SerializeField]
	float gibPower = 3,
		gibTorquePower = 90;
	Rigidbody[] gibs;



	void Start()
	{
		gibs = GetComponentsInChildren<Rigidbody>(true);
	}



	public void InitiateGibs()
	{
		foreach(var gib in gibs)
		{
			gib.gameObject.SetActive(true);

			// randomize rigidbody
			gib.AddForce(Random.onUnitSphere * Random.Range(0, gibPower), ForceMode.VelocityChange);
			gib.AddTorque(Random.onUnitSphere * Random.Range(0, gibTorquePower), ForceMode.VelocityChange);

			// clean
			Destroy(gib.gameObject, Random.Range(3f, 5f));
		}
	}
}
