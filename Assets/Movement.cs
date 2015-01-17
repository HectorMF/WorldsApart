using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	bool moving = false;
	Vector3 target;
	Vector3 dir;
	float speed = 1f;
	
	void Update () 
	{
		if (moving) 
		{
			dir = target - transform.localPosition;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);
		}
		else if (moving && Vector3.Magnitude(dir) < 0.2f)
		{
			moving = false;
			transform.localPosition = target;
		}
	}
	public void Move(Vector3 targetRef)
	{
		target = targetRef;
		moving = true;
	}
}
