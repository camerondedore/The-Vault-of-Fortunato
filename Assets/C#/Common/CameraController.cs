using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	
	/////////////////////////////////////////////////////////////
	// have camera controller execute last in project settings //
	/////////////////////////////////////////////////////////////

	public float smoothTime = 4,
		smoothCursor = 0;
	public Vector3 startPosition;
	public Quaternion startRotation;
	[SerializeField]
	Transform cameraPoint;
	[SerializeField]
	LayerMask mask;
	Camera mainCamera;
	RaycastHit cameraHit;
	Vector3 positionDirection,
		lookDirection,
		targetPostion;
	float range;



	void Start()
	{
		mainCamera = Camera.main;
		targetPostion = cameraPoint.position;
		positionDirection = transform.InverseTransformDirection(cameraPoint.position - transform.position);
		lookDirection = transform.InverseTransformDirection(cameraPoint.forward);
		startPosition = mainCamera.transform.position;
		startRotation = mainCamera.transform.rotation;
		range = positionDirection.magnitude;
	}



	void LateUpdate()
	{
		Physics.Raycast(transform.position, transform.TransformDirection(positionDirection), out cameraHit, range, mask);

		var collider = cameraHit.collider;

		if (collider != null)
		{
			targetPostion = cameraHit.point - transform.TransformDirection(positionDirection).normalized * 0.1f;
		}
		else
		{
			targetPostion = cameraPoint.position;
		}

		MoveCamera();
	}



	void MoveCamera()
	{
		// check for pause
		if(Time.timeScale <= 0)
		{
			return;
		}

		smoothCursor += Time.fixedDeltaTime / smoothTime;
		smoothCursor = Mathf.Clamp01(smoothCursor);

		// apply smooth new position
		mainCamera.transform.position = Vector3.Lerp(startPosition, targetPostion, smoothCursor);

		// apply smooth new rotation
		mainCamera.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.LookRotation(transform.TransformDirection(lookDirection)), smoothCursor);
	}
}
