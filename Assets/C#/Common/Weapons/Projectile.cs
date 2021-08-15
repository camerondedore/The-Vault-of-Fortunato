using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Projectile : MonoBehaviour
{

	public float speed = 10;
	[SerializeField]
	protected GameObject defaultHitFx;
	[Tooltip("Array of child Transforms to unparent before this GameObject is destroyed.  For example, a TrailRenderer can finish properly if it isn't destroyed with this projectle.")]
	[SerializeField]
	Transform[] savedChildren; 
	[SerializeField]
	LayerMask mask;
    [SerializeField]
	float gravityInfluence = 1;
	protected Vector3 velocity = Vector3.zero;



	protected virtual void Start()
	{
		// init velocity
		velocity += transform.forward * speed;
	}
	


	void FixedUpdate()
	{
		// get values for where the projectile will be next tic
		var deltaGravityForTic = Physics.gravity * gravityInfluence * Time.fixedDeltaTime;
		velocity += deltaGravityForTic;
		var velecityForTic = velocity * Time.fixedDeltaTime;
		
		// detect hit, cast ray to where the projectile will be next tic
		if(!RayDetect())
		{
			// apply translation and rotation
			transform.position += velecityForTic;
			transform.forward = velocity;
		}
		else
		{
			Destroy(gameObject);
		}
	}



	void OnDestroy()
	{
		// remove children that need to persist
		foreach(var t in savedChildren)
		{
			t.parent = null;
		}
	}



	bool RayDetect()
	{
		//Debug.DrawRay(transform.position, velocity * Time.fixedDeltaTime, Random.ColorHSV(), 30);
		var hits = Physics.RaycastAll(transform.position, velocity, velocity.magnitude * Time.fixedDeltaTime, mask);
		hits = hits.OrderBy(h => h.distance).ToArray();
		return OnCast(hits);
	}



	/// <summary>
	/// Handles hit detection, returns if the projectile will be destroyed.
	/// </summary>
	protected virtual bool OnCast(RaycastHit[] hits)
	{
		//////////////////////////////////////////////////////////////
		// for child classes, like bullet, override the OnCast method.
		//////////////////////////////////////////////////////////////
		
		if (hits.Length == 0)
		{
			return false;
		}

		// hit
		transform.position = hits[0].point;

		// FX
		if (defaultHitFx != null)
		{
			Instantiate(defaultHitFx, hits[0].point, Quaternion.LookRotation(hits[0].normal, velocity));
		}

		return true;
	}

}
