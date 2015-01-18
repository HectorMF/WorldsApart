using UnityEngine;
using System.Collections;
using System;
using WorldsApart.Handlers;

public class Movement : MonoBehaviour
{
	public bool moving = false;
	private Vector3 target;
    private Transform movingTarget;
	Vector3 dir;
    Vector3 prevDir;
	float speed = 1f;

    private Action _notify;
	
	void Update () 
	{
        if (movingTarget != null) target = movingTarget.localPosition;
        dir = target - transform.localPosition;

        if (prevDir != null)
        {
            if ((prevDir.x > 0 && dir.x < 0) || (prevDir.x < 0 && dir.x > 0)) transform.Rotate(transform.up, 180f);
        }
        else if (dir.x < 0) transform.Rotate(transform.up, 180f);

        prevDir = dir;
        var animHandler = new SetAnimationFloatHandler();
        animHandler.gameObject = gameObject;
        animHandler.floatName = "Speed";

        if (moving && Vector3.Magnitude(dir) > 0.2f) 
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);
            animHandler.value = 1f;
            animHandler.Invoke();
		}
		else if (moving && Vector3.Magnitude(dir) < 0.2f)
		{
			moving = false;
			transform.localPosition = target;
            animHandler.value = 0f;
            animHandler.Invoke();
            if(_notify != null) _notify();
            movingTarget = null;
			audio.Stop();
		}
	}
	public void Move(Vector3 targetRef)
	{
		target = targetRef;
		moving = true;
		audio.Play();
	}

    public void Move(Vector3 targetRef, Action notify)
    {
        Move(targetRef);
        _notify = notify;
    }

    public void Move(Transform targetRef)
    {
        movingTarget = targetRef;
        moving = true;
        audio.Play();
    }

    public void Move(Transform targetRef, Action notify)
    {
        Move(targetRef);
        _notify = notify;
    }
}
