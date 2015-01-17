using UnityEngine;
using System.Collections;
using System;

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

        if (moving && Vector3.Magnitude(dir) > 0.2f) 
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);
		}
		else if (moving && Vector3.Magnitude(dir) < 0.2f)
		{
			moving = false;
			transform.localPosition = target;
            _notify();
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
