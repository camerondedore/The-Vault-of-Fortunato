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
	Vector3 followPosition;
	Vector2 lookRawChange;
	float range,
		rangeFlat;



    void Start()
    {
        mainCamera = Camera.main.transform;
		root = transform.root;

		var angle = Vector3.Angle(cameraPoint.localPosition, new Vector3(cameraPoint.localPosition.x, 0, cameraPoint.localPosition.z));
		range = Vector3.Distance(transform.position, cameraPoint.position);
		rangeFlat = range * Mathf.Cos(angle * Mathf.PI / 180);

		followPosition = mainCamera.position;

		// initialize camera look
		LateUpdate();
		mainCamera.forward = cameraY.position - mainCamera.position;
    }

    

    void LateUpdate()
    {
		// check for pause
		if(Time.timeScale <= 0)
		{
			return;
		}

		// apply look from input
		followPosition += mainCamera.right * -PlayerInput.look.x * 0.5f * Time.deltaTime;

		// tether camera, set horizontal position, then vertical
        var newDirection = followPosition - transform.position;
		newDirection.y = 0;
		followPosition = transform.position + newDirection.normalized * rangeFlat;
		followPosition.y = cameraPoint.position.y;

		// ray check to root
		var rayDirection = followPosition - root.position;
		//rayDirection.y = 0;
		Physics.Raycast(root.position, rayDirection, out cameraHit, range, mask);
		//Debug.DrawRay(root.position, rayDirection.normalized * range);

		var collider = cameraHit.collider;

		if (collider != null)
		{
			// apply ray check, using camera point y
			var newPosition = cameraHit.point - rayDirection.normalized * 0.1f;
			newPosition.y = cameraPoint.position.y;
			//mainCamera.position = Vector3.Lerp(mainCamera.position, newPosition, Time.deltaTime * 6);
			mainCamera.position = newPosition;
		}
		else
		{
			// apply movement to stored vector
			mainCamera.position = followPosition;
			//mainCamera.position = Vector3.Lerp(mainCamera.position, followPosition, Time.deltaTime * 6);
		}

		// apply look
		mainCamera.forward = Vector3.Lerp(mainCamera.forward, cameraY.position - mainCamera.position, Time.deltaTime * 5);

		// update camera point
		var newCameraPointPosition = followPosition;
		newCameraPointPosition.y = cameraPoint.position.y;
		cameraPoint.position = newCameraPointPosition;
		var newCameraPointForward = cameraY.position - cameraPoint.position;
		newCameraPointForward.y = 0;
		cameraPoint.forward = newCameraPointForward;
    }


	public void CenterCamera()
	{
		// tether camera
        var newDirection = characterMesh.forward;
		followPosition = transform.position - newDirection.normalized * range;
		followPosition.y = cameraPoint.position.y;

		// apply look
		mainCamera.forward = cameraY.position - mainCamera.position;
	}
}
