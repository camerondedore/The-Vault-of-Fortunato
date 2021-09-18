using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    
	public static Vector2 move,
		look;
	public static float fire1,
		fire2,
		//aim,
		//reload,
		pause,
		jump,
		centerCamera;



	public void OnFire1(InputValue value)
	{
		fire1 = value.Get<float>();
	}



	public void OnFire2(InputValue value)
	{
		fire2 = value.Get<float>();
	}



	public void OnMove(InputValue value)
	{
		move = value.Get<Vector2>();
	}



	public void OnLook(InputValue value)
	{
		var input = value.Get<Vector2>();
		look = new Vector2(input.x, input.y);
	}



	public void OnJump(InputValue value)
	{
		jump = value.Get<float>();
	}



	public void OnPause(InputValue value)
	{
		pause = value.Get<float>();
	}



	public void OnCenterCamera(InputValue value)
	{
		centerCamera = value.Get<float>();
	}
}
