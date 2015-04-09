using DG.Tweening;
using GoofyGhost.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GoofyGhost.WorldsApart
{
	public class RemoveMilk : MonoBehaviour
	{
		public AudioClip sound;
		void Update()
		{
			if(transform.position.y < -10){
				Destroy(gameObject);
				AudioSource.PlayClipAtPoint(sound, transform.position);
			}
		}
	}
}