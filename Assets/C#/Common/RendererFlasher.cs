using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererFlasher : MonoBehaviour
{
    
	[SerializeField]
	float speed = 10,
		threshold = 0.6f,
		flashTime = 1;
	Renderer rend;
	[HideInInspector]
	public float startTime;



    void Start()
    {
        rend = GetComponent<Renderer>();
		startTime = -flashTime;
    }

    

    void Update()
    {
		if(Time.time > startTime + flashTime)
		{
			rend.enabled = true;
			return;
		}

        if(rend.enabled && Mathf.PingPong(Time.time * speed, 1) >= threshold)
		{
			// hide
			rend.enabled = false;
		}

		if(!rend.enabled && Mathf.PingPong(Time.time * speed, 1) < threshold)
		{
			// show
			rend.enabled = true;
		}
    }
}
