using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{

	Transform mainCamera;
	AudioSource aud;



    void Start()
    {
        mainCamera = Camera.main.transform;

		// init audio
		aud = GetComponent<AudioSource>();
		if(aud.clip != null)
		{
			aud.time = Random.Range(0, aud.clip.length);
    	}
	}



    void LateUpdate()
    {
		transform.position = mainCamera.transform.position;
    }
}
