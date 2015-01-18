using UnityEngine;
using System.Collections;
using System;
using WorldsApart.Handlers;

public class CarMovement : MonoBehaviour 
{
	Vector3 target;
	Vector3 dir;
	Vector3? prevDir = null;

	float speed = 1f;

	void Update () 
	{
		var animHandler = new SetAnimationFloatHandler();
		animHandler.gameObject = gameObject;
		animHandler.floatName = "Speed";

		dir = target - transform.position;
		if (prevDir != null)
		{
			if ((prevDir.Value.x > 0 && dir.x < 0) || (prevDir.Value.x < 0 && dir.x > 0)) transform.Rotate(transform.up, 180f);
		}
		else if (dir.x < 0) transform.Rotate(transform.up, 180f);

		prevDir = dir;
		
		if (Vector3.Magnitude(dir) > 0.2f) 
		{
			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
			animHandler.value = 1f;
			animHandler.Invoke();
		}
		else if (Vector3.Magnitude(dir) < 0.2f)
			Destroy(gameObject);
	}

	public void DriveTo(Vector3 targetRef)
	{
		target = targetRef;
		audio.Play();
	}
}
