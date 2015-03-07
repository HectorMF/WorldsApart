using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace WorldsApart.GUI
{

    public class Wobble : MonoBehaviour {
    	public float duration = 1;
    	public float timeBetweenShakes = 4;
        public float strength = .2f;
        public int vibration = 4;
        public WobbleMode mode = WobbleMode.Timed;
    	private float timer;
    	
    	// Update is called once per frame
    	void Update () {
            if (mode == WobbleMode.Timed)
            {
                timer += Time.deltaTime;
                if (timer >= timeBetweenShakes)
                {
                    timer -= timeBetweenShakes;
                    TriggerWobble();
                }
            }
    	}

        internal void TriggerWobble()
        {
            transform.DOShakeScale(duration, strength, vibration);
            //transform.DOShakePosition(duration,.1f,4);
        }
    }

    public enum WobbleMode
    {
        Timed,
        Trigger
    }
}