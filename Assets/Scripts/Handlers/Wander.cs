using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using WorldsApart.Handlers;
using DG.Tweening;

namespace WorldsApart.Utility
{
	public class Wander : MonoBehaviour
	{
		public Rect zone;
		public Ease easingFunction = Ease.InOutSine;
		public float walkSpeed = 1.5f;
		public float turnSpeed = 1;
		public float minDelay = 5;
		public float maxDelay = 10;
		public float minDeltaDistance = 0;

		private Vector3 target;
		private bool isMoving;
		private float waitTimer;
		private float duration;
		private Animator animator;

		void Start()
		{
			animator = GetComponent<Animator>();
			FinishWalking();
			CalculateTarget();
			transform.position = target;
		}

		public void SetTarget(Vector3 newTarget)
		{
			transform.DOKill();
			transform.DOMove(newTarget, duration).SetEase(easingFunction).OnComplete(FinishWalking);
		}

		private void CalculateTarget()
		{
			float x = Random.Range(zone.x, zone.x + zone.width);
			float y = Random.Range(zone.y, zone.y + zone.height);
			target = new Vector3(x, y, y);

			while((target - transform.position).magnitude < minDeltaDistance)
			{
				x = Random.Range(zone.x, zone.x + zone.width);
				y = Random.Range(zone.y, zone.y + zone.height);
				target = new Vector3(x, y, y);
			}

			duration = (target - transform.position).magnitude / walkSpeed;
		}

		private void FinishWalking()
		{
			isMoving = false;
			waitTimer = Random.Range(minDelay, maxDelay);
			if (animator != null) 
				animator.SetFloat("Speed", 0);
		}

		void Update ()
		{
			if (!isMoving)
			{
				waitTimer -= Time.deltaTime;
                
				if (waitTimer <= 0) //done waiting
				{
					isMoving = true;
					CalculateTarget();
					if (animator != null) 
						animator.SetFloat("Speed", 1);
					transform.DOMove(target, duration).SetEase(easingFunction).OnComplete(FinishWalking);
					if(target.x > transform.position.x)
						transform.DORotate(new Vector3(0,0,0), turnSpeed);
					else
						transform.DORotate(new Vector3(0,180,0), turnSpeed);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(zone.center, zone.size);
		}
	}
}