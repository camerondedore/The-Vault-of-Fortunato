using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MobTools
{
    




	/// <summary>
	/// Get the closest actor, based on distance, that isn't the same faction as you.
	/// </summary>
	public static Faction GetClosestEnemy(Faction me)
	{
		var sortedActors = (from actor in Faction.actors
							where actor.faction != me.faction
							orderby (actor.transform.position - me.transform.position).sqrMagnitude
							select actor).ToArray();
		
		if(sortedActors.Length > 0)
		{
			return sortedActors[0];
		}

		return null;		
	}



	/// <summary>
	/// Get the closest actor, based on distance and line of sight, that isn't the same faction as you.
	/// </summary>
	public static Faction GetClosestEnemy(Faction me, Vector3 eyePosition, LayerMask mask)
	{
		var sortedActors = (from actor in Faction.actors
							where actor.faction != me.faction
							orderby (actor.transform.position - me.transform.position).sqrMagnitude
							select actor).ToArray();

		foreach(var actor in sortedActors)
		{
			if(CheckLOS(eyePosition, actor.gameObject, mask))
			{
				return actor;
			}
		}

		return null;		
	}



	/// <summary>
	/// Check if you have a line of sight to the target.
	/// </summary>
	public static bool CheckLOS(Vector3 start, GameObject target, LayerMask mask)
	{
		RaycastHit hit;
		var direction = target.transform.position - start;
		Physics.Raycast(start, direction, out hit, direction.magnitude, mask);

		var co = hit.collider;
		if(co != null && co.gameObject == target)
		{
			return true;
		}

		return false;
	}



	/// <summary>
	/// Check if target is within sight cone.
	/// </summary>
	public static bool CheckSightAngle(Vector3 start, Vector3 forward, Vector3 target, float maxAngle)
	{
		var direction = target - start;
		return Vector3.Angle(direction, forward) <= maxAngle;
	}



	/// <summary>
	/// Check if target is within range
	/// </summary>
	public static bool CheckDistance(Vector3 start, Vector3 target, float distance)
	{
		return Vector3.Distance(start, target) <= distance;
	}
}
