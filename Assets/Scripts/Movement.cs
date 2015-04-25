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
    Vector3 prevDir = new Vector3(-999, -999, -999);
	public float speed = 1f;
    private AudioSource audio;

    private Action _notify;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
	
	void Update () 
	{
        if (movingTarget != null) target = movingTarget.position;
        dir = target - transform.position;

        if (prevDir.x != -999 && prevDir.y != -999 && prevDir.z != -999)
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
			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            animHandler.value = 1f;
            animHandler.Invoke();
		}
		else if (moving && Vector3.Magnitude(dir) < 0.2f)
		{
			moving = false;
			transform.position = target;
            animHandler.value = 0f;
            animHandler.Invoke();
            if(_notify != null) _notify();
            movingTarget = null;
			if (audio != null) audio.Stop();
		}
	}

	public void Move(Vector3 targetRef)
	{
		target = targetRef;
		target = new Vector3(target.x, target.y, target.y);
		moving = true;
        if (audio != null) audio.Play();
	}

    public void Move(Vector3 targetRef, Action notify)
    {
        Move(targetRef);
        _notify = notify;
    }

    public void Move(Transform targetRef)
    {
        movingTarget = targetRef;
//		movingTarget = new Vector3(movingTarget.x, movingTarget.y, movingTarget.y);
        moving = true;
        if (audio != null) audio.Play();
    }

    public void Move(Transform targetRef, Action notify)
    {
        Move(targetRef);
        _notify = notify;
    }
}
