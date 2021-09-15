using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
	/////////////////////////////////////////////////////
	// This needs to be on character animator gameobject
	/////////////////////////////////////////////////////
    
	[SerializeField]
	AudioSourceController aud;
	[SerializeField]
	GroundChecker feet;
	public AudioClip[] stepSounds;
	public AudioClip[] stepSoundsTerrain;
	public AudioClip slideSound,
		jumpSound,
		landSound,
		dieSound;
	int stepSoundIndex = 0;



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



	public void SlideStart()
	{
		aud.source.clip = slideSound;
		aud.source.Play();
	}



	public void SlideStop()
	{
		aud.source.clip = null;
		aud.source.Stop();
	}



	public void PlayJump()
	{
		aud.PlayOneShot(jumpSound);
	}



	public void PlayLand()
	{
		aud.PlayOneShot(landSound);
	}



	public void PlayDie()
	{
		aud.PlayOneShot(dieSound);
	}
}
