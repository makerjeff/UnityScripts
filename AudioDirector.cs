using UnityEngine;
using System.Collections;

public class AudioDirector : MonoBehaviour {

	public int currentClipIndex = 0;

	//create an array with 12 spots for clips
	public AudioClip[] clips = new AudioClip[12];
	public OSPAudioSource OSPspeaker;

	private AudioSource barrySource;
	private AudioClip currentClip;

	// Use this for initialization
	void Start () {

		//define OSPspeaker
		barrySource = OSPspeaker.GetComponent<AudioSource>();
		currentClip = OSPspeaker.GetComponent<AudioSource>().clip;

		Debug.Log ("source: " + barrySource);
		Debug.Log("clip: " + currentClip);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(barrySource.isPlaying == false) {
			Debug.Log ("playing next clip: " + clips[currentClipIndex]);

			OSPspeaker.GetComponent<AudioSource>().clip = clips[currentClipIndex];
			//currentClip = clips[currentClipIndex];
			OSPspeaker.GetComponent<AudioSource>().Play();

			currentClipIndex ++;

			if(currentClipIndex == 12) {
				currentClipIndex = 0;
			}

		
		}


	}
}
