using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFalloff : MonoBehaviour
{
    
	[Tooltip("create different falloff curves for each quality level")]
	[SerializeField]
	AnimationCurve[] qualityfalloffCurves;
	Transform mainCamera;
	Light lamp;



    void Start()
    {
		mainCamera = Camera.main.transform;
		lamp = GetComponent<Light>();
    }



    void Update()
    {
		var distaneToCamera = Vector3.Distance(transform.position, mainCamera.position);
		var falloff = qualityfalloffCurves[QualitySettings.GetQualityLevel()].Evaluate(distaneToCamera);
        lamp.shadowStrength = falloff;

		if(falloff <= 0 && lamp.shadows != LightShadows.None)
		{
			lamp.shadows = LightShadows.None;
		}

		if(falloff > 0 && lamp.shadows == LightShadows.None)
		{
			lamp.shadows = LightShadows.Hard;
		}
    }
}
