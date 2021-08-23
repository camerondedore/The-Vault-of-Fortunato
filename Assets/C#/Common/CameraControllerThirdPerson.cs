using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerThirdPerson : MonoBehaviour
{

	/////////////////////////////////////////////////////////////
	// have camera controller execute last in project settings //
	/////////////////////////////////////////////////////////////
    
	[SerializeField]
	Transform characterMesh,
		cameraPoint,
		cameraY;
	[SerializeField]
	LayerMask mask;
	Transform mainCamera,
		root;
	RaycastHit cameraHit;
	float range,
		rangeFlat;



    void Start()
    {
        mainCamera = Camera.main.transform;
		root = transform.root;

		var angle = Vector3.Angle(cameraPoint.localPosition, new Vector3(cameraPoint.localPosition.x, 0, cameraPoint.localPosition.z));
		range = Vector3.Distance(transform.position, cameraPoint.position);
		rangeFlat = range * Mathf.Cos(angle * Mathf.PI / 180);
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
		mainCamera.LookAt(cameraY);

		// apply movement
		mainCamera.position = newPosition;

		// ray check to root, flatten on y plane
		var rayDirection = mainCamera.position - root.position;
		rayDirection.y = 0;
		Physics.Raycast(root.position, rayDirection, out cameraHit, rangeFlat, mask);
		//Debug.DrawRay(root.position, rayDirection.normalized * rangeFlat);

		var collider = cameraHit.collider;

		if (collider != null)
		{
			// apply ray check, using camera point y
			newPosition = cameraHit.point;
			newPosition.y = cameraPoint.position.y;
			mainCamera.position = newPosition;
		}
    }


	public void CenterCamera()
	{
		// tether camera
        var newDirection = characterMesh.forward;
		var newPosition = transform.position - newDirection.normalized * range;
		newPosition.y = cameraPoint.position.y;

		// apply movement
		mainCamera.position = Vector3.Lerp(mainCamera.position, newPosition, Time.fixedDeltaTime * 10);
	}
}
