using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    
	[SerializeField]
	AudioSourceController aud;
	[SerializeField]
	AudioClip buttonSound;



	public void PlayButtonSound()
	{
		aud.source.pitch = 1;
		aud.PlayOneShot(buttonSound);
	}



	public void PlayButtonUpSound()
	{
		aud.source.pitch = 1.1f;
		aud.PlayOneShot(buttonSound);
	}



	public void PlayButtonDownSound()
	{
		aud.source.pitch = 0.9f;
		aud.PlayOneShot(buttonSound);
	}
}
