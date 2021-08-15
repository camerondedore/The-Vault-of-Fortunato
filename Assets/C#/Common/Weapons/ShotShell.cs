using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotShell : MonoBehaviour, IAffectable
{
    
	[SerializeField]
	GameObject shot;
	[Tooltip("Grouping radius at 10m")]
	[SerializeField]
	float spread = 1;
	[SerializeField]
	int shotCount = 8;
	Vector3 inheritedVelocity;
	float barrelEffectivenessMultiplier;
	


	void Start()
	{
		int count = shotCount;
		while(shotCount > 0)
		{
			// calculate inaccuracy
			var startForward = transform.forward * 10 + Random.insideUnitSphere * spread;

			// create shot
			var s = Instantiate(shot, transform.position, Quaternion.LookRotation(startForward)) as GameObject;
			var affectable = s.GetComponent<IAffectable>();
			affectable.Affect(barrelEffectivenessMultiplier, inheritedVelocity);

			shotCount--;
		}

		Destroy(gameObject);
	}



	public void Affect(float multiplier, Vector3 inheritedVel)
	{
		spread *= multiplier;
		barrelEffectivenessMultiplier = multiplier;
		inheritedVelocity = inheritedVel;
	}
}
