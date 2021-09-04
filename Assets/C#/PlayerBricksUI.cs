using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBricksUI : MonoBehaviour
{
    
	[SerializeField]
	PlayerBricks bricks;
	[SerializeField]
	Text brickCounter;
	[SerializeField]
	RectTransform animatedBrick,
		animatedBrickTarget;
	[SerializeField]
	AudioSourceController aud;
	[SerializeField]
	AudioClip brickPickupSound;
	Vector3 animatedBrickStartPosition;
	float animatedBrickLerpValue = 1;
	int oldBrickCount = -1;



	void Start()
	{
		animatedBrickStartPosition = animatedBrick.position;
		oldBrickCount = bricks.bricksCollected;
	}



	void FixedUpdate()
	{
		// update UI
		if(oldBrickCount != bricks.bricksCollected)
		{
			oldBrickCount = bricks.bricksCollected;
			brickCounter.text = $"{bricks.bricksCollected}/{bricks.totalBricks}";

			// reset brick animation
			animatedBrickLerpValue = 0;

			// play audio
			aud.PlayOneShot(brickPickupSound);
		}

		// animate Brick
		animatedBrickLerpValue = Mathf.Clamp01(animatedBrickLerpValue + Time.deltaTime * 3);
		animatedBrick.position = Vector3.Lerp(animatedBrickStartPosition, animatedBrickTarget.position, animatedBrickLerpValue);
	}
}
