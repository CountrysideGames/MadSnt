using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class AudioManager : MonoBehaviour {

	public GameObject[] audioClips;
	private int lastNice = 0;
	private int lastAngry = 0;
	private int lastScream = 0;
	
	public void Play (string audioClipName) {
		if (audioClipName == "shot") {
			audioClips[Random.Range(4, 6)].audio.Play ();
		}
		else if (audioClipName == "nice"){
			var randomNice = Random.Range(7, 17);

			while (randomNice == lastNice)
				randomNice = Random.Range(7, 17);

			lastNice = randomNice;

			audioClips[randomNice].audio.Play ();
		}
		else if (audioClipName == "scream"){
			var randomNice = Random.Range(2, 4);
			
			while (randomNice == lastScream)
				randomNice = Random.Range(21, 25);
			
			lastScream = randomNice;
			
			audioClips[randomNice].audio.Play ();
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
