using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IWeaponAction
{
   
	[SerializeField]
	GameObject bullet;
	[Tooltip("Grouping radius at 100m.  Increases with continued fire. Time axis represents rounds fired.")]
	[SerializeField]
	AnimationCurve spreadCurve,
		movementSpreadCurve;
	[SerializeField]
	float recoverySpeed = 1,
		timeToRecover = 1;
	[Space(10)]
	[Header("Values Applied to Bullet")]
	[SerializeField]
	float barrelEffectivenessMultiplier = 1;
	VelocityTracker tracker;
	float spreadCursor = 0,
		spreadStartTime = -Mathf.Infinity,
		roundsToMaxSpread,
		speedToMaxSpread;



	void Start()
	{
		// get max value for spread cursor
		roundsToMaxSpread = spreadCurve.keys[spreadCurve.length - 1].time;
		speedToMaxSpread = movementSpreadCurve.keys[movementSpreadCurve.length - 1].time;
		tracker = transform.root.GetComponent<VelocityTracker>();
	}



	void FixedUpdate()
	{
		if(Time.time > spreadStartTime + timeToRecover && spreadCursor > 0)
		{
			// recover
			spreadCursor = Mathf.Clamp(spreadCursor - Time.fixedDeltaTime * recoverySpeed, 0, roundsToMaxSpread);
		}
	}



	public void Fire()
	{
		// calculate inaccuracy
		var randomForward = transform.forward * 100 + Random.insideUnitSphere * GetSpread();

		// create bullet
		var b = Instantiate(bullet, transform.position, Quaternion.LookRotation(randomForward)) as GameObject;
		var affectable = b.GetComponent<IAffectable>();
		affectable.Affect(barrelEffectivenessMultiplier, tracker.velocity);

		// update spread start time
		spreadStartTime = Time.time;
		spreadCursor = Mathf.Clamp(spreadCursor + 1, 0, roundsToMaxSpread);
	}



	public float GetSpread()
	{
		return spreadCurve.Evaluate(spreadCursor) + 
			movementSpreadCurve.Evaluate(Mathf.Clamp(tracker.speed, 0, speedToMaxSpread));
	}



	public void SetForward(Vector3 direction)
	{
		transform.forward = direction;
	}
}