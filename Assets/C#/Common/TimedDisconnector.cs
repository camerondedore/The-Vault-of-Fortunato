using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDisconnector
{

	public float releaseTime = 1;
	float hookTime = -Mathf.Infinity;



	/// <summary>
	/// Trips the disconnector.
	/// </summary>
	public void Trip()
	{
		if (Time.time > hookTime + releaseTime)
		{
			hookTime = Time.time;
		}
	}



	/// <summary>
	/// Returns if an action can happen.
	/// </summary>
	public bool CanTrip()
	{
		return Time.time > hookTime + releaseTime;
	}
}
