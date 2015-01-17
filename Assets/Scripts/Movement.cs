using UnityEngine;
using System.Collections;
using System;
using WorldsApart.Handlers;

public class Movement : MonoBehaviour
{
	bool moving = false;
	Vector3 target;
	Vector3 dir;
	float speed = 1f;

    private Action _notify;
	
	void Update () 
	{
        dir = target - transform.localPosition;

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
		}
	}
	public void Move(Vector3 targetRef)
	{
		target = targetRef;
		moving = true;
	}

    public void Move(Vector3 targetRef, Action notify)
    {
        Move(targetRef);
        _notify = notify;
    }
}
