using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile, IAffectable
{
    
	[Space(10)]
	[Header("Bullet Properties")]
	public float damage = 35,
		penetration = 0;
	[SerializeField]
	bool inheritVelocity = true;



	protected override bool OnCast(RaycastHit[] hits)
	{
		if (hits.Length == 0)
		{
			return false;
		}

		// hit something
		foreach (var hit in hits)
		{	
			// damage
			var damageable = hit.collider.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.Hit(damage);
			}

			// rigidbody
			var rb = hit.collider.GetComponent<Rigidbody>();
			if (rb != null)
			{
				StartCoroutine(ApplyHitForce(rb, velocity, hit.point, damage));
			}

			// FX and penetration
			var colliderMaterial = hit.collider.GetComponent<MaterialHitFx>();
			if (colliderMaterial != null)
			{
				// FX for front
				var fxFront = Instantiate(colliderMaterial.hitFxFront, hit.point, Quaternion.LookRotation(hit.normal, velocity)) as GameObject;
				fxFront.transform.parent = hit.collider.transform;
				
				// penetration check
				penetration -= colliderMaterial.penetrationResistance;
				if(penetration <= 0)
				{
					// too much material to penetrate
					transform.position = hit.point;
					return true;
				}
				else
				{
					// penetration
					// FX for back
					var fxBack = Instantiate(colliderMaterial.hitFxBack, hit.point + velocity.normalized * 0.02f, Quaternion.LookRotation(-hit.normal, velocity)) as GameObject;
					fxBack.transform.parent = hit.collider.transform;
				}
			}
			else
			{
				if (defaultHitFx != null)
				{
					Instantiate(defaultHitFx, hit.point, Quaternion.LookRotation(hit.normal, Random.onUnitSphere));
				}
				
				// no penetration
				transform.position = hit.point;
				return true;
			}
		}

		// penetrated all hits
		return false;
	}



	IEnumerator ApplyHitForce(Rigidbody rb, Vector3 direction, Vector3 point, float adamage)
	{
		yield return new WaitForFixedUpdate();
		var force = direction.normalized * damage * 0.25f;
		
		if(rb != null)
		{
			rb.AddForceAtPosition(force, point, ForceMode.Impulse);
		}
	}



	public void Affect(float multiplier, Vector3 inheritedVelocity)
	{
		damage *= multiplier;
		speed *= multiplier;
		penetration *= multiplier;
		if(inheritVelocity)
		{
			velocity += inheritedVelocity;
		}
	}
}
