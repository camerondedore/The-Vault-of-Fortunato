using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

	[SerializeField]
	float life = 1;



    void Start()
    {
        Destroy(gameObject, life);
    }
}
