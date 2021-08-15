using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerThirdPerson : MonoBehaviour
{

	/////////////////////////////////////////////////////////////
	// have camera controller execute last in project settings //
	/////////////////////////////////////////////////////////////
    
	[SerializeField]
	Transform character;
	[SerializeField]
	LayerMask mask;
	Transform mainCamera,
		cameraPoint,
		root;
	RaycastHit cameraHit;
	float range;



    void Start()
    {
        mainCamera = Camera.main.transform;
		cameraPoint = transform.GetChild(0);
		root = transform.root;

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
		mainCamera.position = newPosition;

		// ray check
		var rayDirection = mainCamera.position - root.position;
		rayDirection.y = 0;
		Physics.Raycast(root.position, rayDirection, out cameraHit, range, mask);

		var collider = cameraHit.collider;

		if (collider != null)
		{
			// apply ray check
			newPosition = cameraHit.point;
			newPosition.y = cameraPoint.position.y;
			mainCamera.position = newPosition;
		}
    }


	public void CenterCamera()
	{
		// tether camera
        var newDirection = character.forward;
		var newPosition = transform.position - newDirection.normalized * range;
		newPosition.y = cameraPoint.position.y;

		// apply movement
		mainCamera.position = Vector3.Lerp(mainCamera.position, newPosition, Time.fixedDeltaTime * 10);
	}
}
