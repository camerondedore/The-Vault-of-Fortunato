using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{

	[HideInInspector]
	public AudioSource source;
	public bool realTime = true;



	void Awake()
	{
		source = GetComponent<AudioSource>();
	}



	void Update()
	{
		if(realTime)
		{
			source.pitch = Time.timeScale;
		}
	}



	public void PlayOneShot(AudioClip clip)
	{
		source.PlayOneShot(clip);
	}



	public int PlayOneShotFromArray(AudioClip[] clips, int index)
	{
		var oldIndex = index;
		while(oldIndex == index)
		{
			index = Random.Range(0, clips.Length);
		}

		var clip = clips[index];
		source.PlayOneShot(clip);
		return index;
	}



	public void RandomizePitch(float radius)
	{
		source.pitch = 1 + Random.Range(-radius, radius);
	}



	public void RandomizeVolume(float radius)
	{
		source.volume = 1 + Random.Range(-radius, radius);
	}
}
