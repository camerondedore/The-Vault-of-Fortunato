using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAudio : MonoBehaviour
{
    /////////////////////////////////////////////////////
	// This needs to be on character animator gameobject
	/////////////////////////////////////////////////////
    
	[SerializeField]
	AudioSourceController aud;
	[SerializeField]
	GroundCheckerNav feet;
	public AudioClip[] stepSounds;
	public AudioClip[] stepSoundsTerrain;
	public AudioClip dieSound,
		meleeHitSound;
	public AudioClip[] meleeSounds;
	int stepSoundIndex = 0,
		meleeSoundIndex = 0;



	void Start()
	{
		
	}



	public void Step()
	{
		if(feet.checkFeet.collider == null && feet.checkFeetRay.collider == null)
		{
			// not on ground
			return;
		}

		var col = feet.checkFeet.collider != null ? feet.checkFeet.collider : feet.checkFeetRay.collider;

		if(col.GetComponent<Terrain>() == null)
		{
			// hard surface
			stepSoundIndex = aud.PlayOneShotFromArray(stepSounds, stepSoundIndex);
		}
		else
		{
			// terrain
			stepSoundIndex = aud.PlayOneShotFromArray(stepSoundsTerrain, stepSoundIndex);
		}
	}



	public void Melee()
	{
		meleeSoundIndex = aud.PlayOneShotFromArray(meleeSounds, meleeSoundIndex);
	}



	public void MeleeHit()
	{
		aud.PlayOneShot(meleeHitSound);
	}



	public void PlayDie()
	{
		aud.PlayOneShot(dieSound);
	}
}
