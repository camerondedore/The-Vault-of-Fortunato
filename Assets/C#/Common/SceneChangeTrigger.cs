using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    
	//[SerializeField]
	//Animator uiAnim;
	//[SerializeField]
	//AudioSourceController aud;
	[SerializeField]
	string nextLevelName = "";
	//[SerializeField]
	//float animTime = 1.75f;
	//float triggerTime = Mathf.Infinity;
	// loading = false;



	void Update()
	{
		// if(loading && triggerTime + animTime < Time.time)
		// {
		// 	SceneLoader.LoadLevel(nextLevelName);
		// }
	}



	void OnTriggerEnter()
	{
		SceneLoader.LoadLevel(nextLevelName);
	}
}
