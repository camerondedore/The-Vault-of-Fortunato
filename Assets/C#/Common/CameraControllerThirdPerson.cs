using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerThirdPerson : MonoBehaviour
{

	/////////////////////////////////////////////////////////////
	// have camera controller execute last in project settings //
	/////////////////////////////////////////////////////////////
    
	[SerializeField]
	LayerMask mask;
	Transform mainCamera,
		cameraPoint;
	RaycastHit cameraHit;
	float range;



    void Start()
    {
        mainCamera = Camera.main.transform;
		cameraPoint = transform.GetChild(0);

		range = Vector3.Distance(transform.position, cameraPoint.position);
    }

    

    void LateUpdate()
    {
		// check for pause
		if(Time.timeScale <= 0)
		{
			return;
		}

		// tether camera
        var newDirection = mainCamera.position - transform.position;
		var newPosition = transform.position + newDirection.normalized * range;
		newPosition.y = cameraPoint.position.y;

		// apply look
		mainCamera.LookAt(transform);

		// apply movement
		mainCamera.position = Vector3.Lerp(mainCamera.position, newPosition, Time.fixedDeltaTime * 10);

		// ray check
		var rayDirection = mainCamera.position - transform.position;
		Physics.Raycast(transform.position, rayDirection, out cameraHit, range, mask);

		var collider = cameraHit.collider;

		if (collider != null)
		{
			// apply ray check
			mainCamera.position = cameraHit.point + rayDirection.normalized * -0.1f;
		}
    }
}
