﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class AudioManager : MonoBehaviour {

	public GameObject[] audioClips;
	private int lastNice = 0;
	private int lastAngry = 0;
	private int lastScream = 0;
	private int lastFloor = 0;
	
	public void Play (string audioClipName) {
		if (audioClipName == "shot") {
			audioClips[Random.Range(4, 6)].audio.Play ();
		}
		else if (audioClipName == "nice"){ //NICE
			var randomNice = Random.Range(7, 17);

			while (randomNice == lastNice)
				randomNice = Random.Range(7, 17);

			lastNice = randomNice;

			audioClips[randomNice].audio.Play ();
		}
		else if (audioClipName == "scream"){ //SCREAM
			var randomScream = Random.Range(21, 25);
			
			while (randomScream == lastScream)
				randomScream = Random.Range(21, 25);
			
			lastScream = randomScream;
			
			audioClips[randomScream].audio.Play ();
		}
		else if (audioClipName == "floor"){ //FLOOR

			var randomFloor = Random.Range(21, 25);

			if (Game.levelNumber < 9) { //CAMPO
				randomFloor = Random.Range(25, 28);
			
				while (randomFloor == lastFloor)
					randomFloor = Random.Range(25, 28);
			}
			else { //CIDADE
				randomFloor = Random.Range(27, 30);
				
				while (randomFloor == lastFloor)
					randomFloor = Random.Range(27, 30);
			}
			
			lastFloor = randomFloor;
			
			audioClips[randomFloor].audio.Play ();
		}
		else {
			for (int i = 0; i < audioClips.Length; i++)
				if (audioClips[i].name == audioClipName && audioClips[i] != null)
					audioClips[i].audio.Play ();
		}
	}

	public void PlayWithDelay (string audioClipName, float waitTime) {
		for (int i = 0; i < audioClips.Length; i++)
			if (audioClips[i].name == audioClipName)
				StartCoroutine (PlayClipWithDelay (i, waitTime));
	}

	public IEnumerator PlayClipWithDelay (int audioClip, float waitTime) {
		yield return new WaitForSeconds(waitTime);
		audioClips[audioClip].audio.Play ();
	}

	
	public void Pause (string audioClipName) {
		for (int i = 0; i < audioClips.Length; i++)
			if (audioClips[i].name == audioClipName)
				audioClips[i].audio.Pause ();
	}
}
