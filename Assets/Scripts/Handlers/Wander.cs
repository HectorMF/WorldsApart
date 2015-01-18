using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using System;

namespace WorldsApart.Handlers
{
	public class Wander : MonoBehaviour
	{
		Vector3 distance = new Vector3 (2f,0f,0f);
		Vector3 target;
		Vector3 startPos;
		float startTime;
		float fracJourney;
		float travelDist;
		float waitTimer;
		float speed = 1.5f;
		int turn;
		bool waiting;

        SetAnimationFloatHandler animHandler;

		void Start()
		{
			target = transform.localPosition;
			travelDist = 0f;
			turn = 0;
            animHandler = new SetAnimationFloatHandler();
            animHandler.gameObject = gameObject;
            animHandler.floatName = "Speed";
		}
		void Update ()
		{
			if (waiting) // At target pos waiting
			{
				waitTimer += Time.deltaTime;
                
				if (waitTimer >= 3f) //done waiting
				{
					waiting = false;
					waitTimer = 0f;
                    turn = UnityEngine.Random.Range(-1, 1);
					travelDist = 0f;
					startPos = transform.localPosition;
                    animHandler.value = 1f;
                    animHandler.Invoke();
					Target();
				}
			}
            else if (travelDist < Math.Abs(distance.x)) // Traveling to target pos
            {
                travelDist += Time.deltaTime * speed;
                fracJourney = travelDist / Math.Abs(distance.x);
                transform.localPosition = Vector3.Lerp(startPos, target, fracJourney);
            }
            else // At target pos, start waiting
            {
                animHandler.value = 0f;
                animHandler.Invoke();
                waiting = true;
            }
		}
		void Target()
		{
            if (turn < 0)
            {
                distance = -distance;
                transform.Rotate(transform.up, 180f);
            }

            target = transform.localPosition + distance;
		}
	}
}