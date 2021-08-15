using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Weapon actions such as muzzle flash and casing eject should implement this.
/// </summary>
public interface IWeaponAction
{
	void Fire();
}



public class Weapon : MonoBehaviour
{
    [HideInInspector]
	public int ammo;
	[SerializeField]
	float cycleTime = 0.1f;
	[SerializeField]
	bool automatic = false;
	[SerializeField]
	int magazineCapacity = 30;
	IWeaponAction[] actions;
	public float recoilMagnitude = 1;
	Disconnector sear = new Disconnector();
	TimedDisconnector autoSear = new TimedDisconnector();



	void Start()
	{
		// set up auto sear
		autoSear.releaseTime = cycleTime;
		// load magazine
		ammo = magazineCapacity;
		// get child actions
		actions = GetComponentsInChildren<IWeaponAction>();
	}



	public bool PullTrigger(float trigger)
	{
		if(ammo == 0)
		{
			return false;
		}

		// sear checks
		var isCycleDone = autoSear.CanTrip();
		var isTriggerUp = sear.Trip(trigger);

		// cycle rate check
		if(isCycleDone)
		{
			if(automatic && trigger > 0)
			{
				// auto
				TriggerActions();
				autoSear.Trip();
				ammo = Mathf.Clamp(ammo - 1, 0, magazineCapacity);
				return true;
			}
			else if(isTriggerUp)
			{
				// semi
				TriggerActions();
				autoSear.Trip();
				ammo = Mathf.Clamp(ammo - 1, 0, magazineCapacity);
				return true;
			}
		}

		// cannot fire
		return false;
	}



	public bool CanReload()
	{
		return ammo < magazineCapacity && autoSear.CanTrip();
	}



	public bool IsEmpty()
	{
		return ammo == 0;
	}



	public void StartReload()
	{
		ammo = 0;
	}



	public void Reload()
	{
		ammo = magazineCapacity;
	}



	void TriggerActions()
	{
		foreach(var a in actions)
		{
			a.Fire();
		}
	}
}
