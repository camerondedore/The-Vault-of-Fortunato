using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    
    public Vector3 moveDirection;
	public Vector2 look;
	public float jump,
		fire1,
		fire2;



    void Update()
    {	
		moveDirection = new Vector3(PlayerInput.move.x, 0, PlayerInput.move.y);
		look = PlayerInput.look;
		jump = PlayerInput.jump;
		fire1 = PlayerInput.fire1;
		fire2 = PlayerInput.fire2;
    }



	void OnDisable()
	{
		moveDirection = Vector3.zero;
		look = Vector2.zero;
		jump = 0;
		fire1 = 0;
		fire2 = 0;
	}
}
