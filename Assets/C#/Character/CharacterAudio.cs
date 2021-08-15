using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    
	public AudioClip[] stepSounds;
	public AudioClip slideSound,
		jumpSound,
		landSound,
		dieSound;
	[SerializeField]
	AudioSourceController audFeet,
		audHead;
	int stepSoundIndex = 0;



	void Start()
	{
		
	}



	public void StepEvent()
	{
		var oldStepSoundIndex = stepSoundIndex;
		while(stepSoundIndex == oldStepSoundIndex)
		{
			stepSoundIndex = Random.Range(0, stepSounds.Length);
		}

		var stepSound = stepSounds[stepSoundIndex];
		audFeet.PlayOneShot(stepSound);
	}



	public void SlideStart()
	{
		audFeet.source.clip = slideSound;
		audFeet.source.Play();
	}



	public void SlideStop()
	{
		audFeet.source.clip = null;
		audFeet.source.Stop();
	}



	public void PlayJump()
	{
		audFeet.PlayOneShot(jumpSound);
	}



	public void PlayLand()
	{
		audFeet.PlayOneShot(landSound);
	}



	public void PlayDie()
	{
		audHead.PlayOneShot(dieSound);
	}
}
