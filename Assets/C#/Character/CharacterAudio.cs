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
		jumpSoundTerrain,
		landSound,
		landSoundTerrain,
		dieSound;
	public AudioClip[] meleeSounds;
	int stepSoundIndex = 0,
		meleeSoundIndex = 0;



	void Start()
	{
		
	}



	bool OnTerrain()
	{
		if(feet.checkFeet.collider == null && feet.checkFeetRay.collider == null)
		{
			// not on ground
			return false;
		}

		var col = feet.checkFeet.collider != null ? feet.checkFeet.collider : feet.checkFeetRay.collider;

		if(col.GetComponent<Terrain>() == null)
		{
			// hard surface
			return false;
		}
		else
		{
			// terrain
			return true;
		}
	}



	public void Step()
	{
		if(OnTerrain())
		{
			// hard surface
			stepSoundIndex = aud.PlayOneShotFromArray(stepSoundsTerrain, stepSoundIndex);
		}
		else
		{
			// terrain
			stepSoundIndex = aud.PlayOneShotFromArray(stepSounds, stepSoundIndex);
		}
	}



	public void Melee()
	{
		meleeSoundIndex = aud.PlayOneShotFromArray(meleeSounds, meleeSoundIndex);
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
		if(OnTerrain())
		{
			// hard surface
			aud.PlayOneShot(jumpSoundTerrain);
		}
		else
		{
			// terrain
			aud.PlayOneShot(jumpSound);
		}
	}



	public void PlayLand()
	{
		if(OnTerrain())
		{
			// hard surface
			aud.PlayOneShot(landSoundTerrain);
		}
		else
		{
			// terrain
			aud.PlayOneShot(landSound);
		}
	}



	public void PlayDie()
	{
		aud.PlayOneShot(dieSound);
	}
}
