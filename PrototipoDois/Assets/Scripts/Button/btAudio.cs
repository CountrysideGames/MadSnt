using UnityEngine;
using System.Collections;

public class btAudio : MonoBehaviour {
	
	public Sprite audioOn;
	public Sprite audioOff;

	void OnMouseDown () {
		if (AudioListener.volume == 1) {
			AudioListener.volume = 0;
			GetComponent<SpriteRenderer>().sprite = audioOff;
		}
		else {
			AudioListener.volume = 1;
			GetComponent<SpriteRenderer>().sprite = audioOn;
		}
		
		Debug.Log ("Audio changed to state: " + AudioListener.volume);
	}


}
