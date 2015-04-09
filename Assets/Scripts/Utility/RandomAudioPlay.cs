using UnityEngine;
using System.Collections.Generic;

public class RandomAudioPlay : MonoBehaviour {
	public bool useAudioSource = true;
	public bool playOnStart;
	public List<AudioClip> audioList;
	public float minTime;
	public float maxTime;

	private float timer;
	private float nextPlay;
	private AudioSource audio;

	void Start (){
		nextPlay = Random.Range(minTime, maxTime);
		if(useAudioSource)
			audio = GetComponent<AudioSource>();
		if(playOnStart)
			PlaySound();
	}

	void Update () {
		timer += Time.deltaTime;
		if(timer >= nextPlay)
		{
			timer -= nextPlay;
			nextPlay = Random.Range(minTime, maxTime);
			PlaySound();
		}
	}

	void PlaySound(){
		if(useAudioSource)
			audio.Play();
		else
		{
			AudioSource.PlayClipAtPoint(audioList[Random.Range(0,audioList.Count)], transform.position);
		}
	}
}
