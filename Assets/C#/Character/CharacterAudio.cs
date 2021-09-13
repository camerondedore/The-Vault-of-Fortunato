﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
	/////////////////////////////////////////////////////
	// This needs to be on character animator gameobject
	/////////////////////////////////////////////////////
    
	[SerializeField]
	AudioSourceController aud;
	public AudioClip[] stepSounds;
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
		stepSoundIndex = aud.PlayOneShotFromArray(stepSounds, stepSoundIndex);
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
