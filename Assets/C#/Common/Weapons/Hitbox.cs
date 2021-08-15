using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour, IDamageable
{
    
	public float weakness = 1;
	protected Health baseHealth;



	protected virtual void Start()
	{
		baseHealth = transform.root.GetComponent<Health>();
	}



	public virtual void Hit(float damage)
	{
		baseHealth.Damage(damage * weakness);
	}
}
