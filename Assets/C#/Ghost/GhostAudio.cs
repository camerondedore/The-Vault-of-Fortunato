using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAudio : MonoBehaviour
{
    
	[SerializeField]
	AudioClip[] attackSounds;
	AudioSourceController aud;
	int attackSoundIndex = 0;



    void Start()
    {
        aud = GetComponent<AudioSourceController>();

		// randomize looping sound
		aud.source.time = Random.Range(0, aud.source.clip.length);
    }

    

    public void PlayAttack()
    {
        attackSoundIndex = aud.PlayOneShotFromArray(attackSounds, attackSoundIndex);
    }
}
