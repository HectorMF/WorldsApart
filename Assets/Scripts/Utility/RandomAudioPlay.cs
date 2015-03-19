using UnityEngine;
using System.Collections;

public class RandomAudioPlay : MonoBehaviour {
	public bool playOnStart;
	public float minTime;
	public float maxTime;

	private float timer;
	private float nextPlay;
	private AudioSource audio;

	void Start (){
		nextPlay = Random.Range(minTime, maxTime);
		audio = GetComponent<AudioSource>();
		if(playOnStart)
			audio.Play();
	}

	void Update () {
		timer += Time.deltaTime;
		if(timer >= nextPlay)
		{
			timer -= nextPlay;
			nextPlay = Random.Range(minTime, maxTime);
			audio.Play();
		}
	}
}
