using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonBlackboard : MonoBehaviour
{

    public StateMachine machine;
	public NavMeshAgent agent;
	public VelocityTracker tracker;
	public State idleState,
		alertState,
		seekState,
		attackState,
		hurtState,
		dieState;
	public Transform player;
	public Animator anim;
	public SkeletonGibs gibber;
	public ParticleSystem boneSplinters;
	public GameObject mesh;
	public SkeletonAudio aud;
}
