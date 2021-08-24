using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlackboard : MonoBehaviour
{

	public StateMachine machine;
	public SuperState groundedSuperState,
		ridePlatformSuperState;
	public State idleSubState,
		moveSubState,
		fallState,
		jumpStateInit,
		jumpState,
		slideState,
		dieState,
		meleeAttackSubState;
	public Transform root;
	public CharacterController agent;
	public CameraControllerThirdPerson cameraController;
	public CharacterInput input;
	public CameraPivotController cameraPivotController;
	public GroundChecker feet;
	public Animator anim;
	public VelocityTracker tracker;
	public CharacterAudio charAud;
	public Transform characterMesh;
	public float speed = 6,
		lookSpeed = 10,
		stepHeight = 0.3f,
		maxSlope = 80; // this maxSlope is for checking if falling should turn into sliding, where as the ground checker maxAngle is for being grounded
	public Melee melee;
	[HideInInspector]
	public float y,
		jumpStartAltitude;
	[HideInInspector]
	public Vector3 targetVelocity,
		velocity,
		lookDirection;
	[HideInInspector]
	public Disconnector jumpDisconnector = new Disconnector(),
		fire1Disconnector = new Disconnector(),
		fire2Disconnector = new Disconnector();
}