using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RawImageAnimation : MonoBehaviour {
	public List<Texture> frames;
	public float delay;
	public bool looping = true;

	private float timer;
	private bool running;
	private RawImage image;
	private int index;

	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage>();
		index = 0;
		image.texture = frames[index];
		timer = delay;
		running = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!running) return;

		timer -= Time.deltaTime;

		if(timer <= 0)
		{
			index++;
			if(index == frames.Count)
			{
				if(looping)
					index = 0;
				else
					running = false;
			}
			timer += delay;
			image.texture = frames[index];
		}
	}
}
