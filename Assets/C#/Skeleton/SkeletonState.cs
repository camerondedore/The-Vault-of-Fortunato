using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonState : State
{

	[HideInInspector]
	public SkeletonBlackboard blackboard;



	protected void Awake()
	{
		blackboard = GetComponent<SkeletonBlackboard>();
	}
}
