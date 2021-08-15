using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{

	Transform mainCamera;



    void Start()
    {
        mainCamera = Camera.main.transform;
    }



    void LateUpdate()
    {
		transform.position = mainCamera.transform.position;
    }
}
