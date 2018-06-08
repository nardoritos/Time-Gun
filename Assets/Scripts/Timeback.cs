using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeback: MonoBehaviour {
	public GameObject player;
	public ArrayList keyframes;
	public bool isReversing = false;

	public int keyframe = 5;
	private int frameCounter = 0;
	private int reverseCounter = 0;

	private Vector3 currentPosition;
	private Vector3 previousPosition;
	private Vector3 currentRotation;
	private Vector3 previousRotation;


	private bool firstRun = true;

	void Start()
	{
		keyframes = new ArrayList();
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.Z))
		{
			isReversing = true;
		}
		else
		{
			isReversing = false;
			firstRun = true;
		}
	}

	void FixedUpdate()
	{
		if(!isReversing)
		{
			if(frameCounter < keyframe)
			{
				frameCounter += 1;
			}
			else
			{
				frameCounter = 0;
				keyframes.Add(new KeyframeInGame(player.transform.position, player.transform.localEulerAngles));
			}
		}
		else
		{
			if(reverseCounter > 0)
			{
				reverseCounter -= 1;
			}
			else
			{
				reverseCounter = keyframe;
				RestorePositions();
			}

			if(firstRun)
			{
				firstRun = false;
				RestorePositions();
			}

			float interpolation = (float) reverseCounter / (float) keyframe;
			player.transform.position = Vector3.Lerp(previousPosition, currentPosition, interpolation);
			player.transform.localEulerAngles = Vector3.Lerp(previousRotation, currentRotation, interpolation);
		}

		if(keyframes.Count > 128)
		{
			keyframes.RemoveAt(0);
		}
	}

	void RestorePositions()
	{
		int lastIndex = keyframes.Count - 1;
		int secondToLastIndex = keyframes.Count - 2;

		if(secondToLastIndex >= 0)
		{
			currentPosition  = (keyframes[lastIndex] as KeyframeInGame).position;
			previousPosition = (keyframes[secondToLastIndex] as KeyframeInGame).position;

			currentRotation  = (keyframes[lastIndex] as KeyframeInGame).rotation;
			previousRotation = (keyframes[secondToLastIndex] as KeyframeInGame).rotation;

			keyframes.RemoveAt(lastIndex);
		}
	}
}


public class KeyframeInGame {
	public Vector3 position;
	public Vector3 rotation;

	public KeyframeInGame(Vector3 position, Vector3 rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}
}

