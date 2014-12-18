using UnityEngine;
using System.Collections;

public class ButtonAudio : MonoBehaviour {
	
	void OnMouseDown () {
		Menu.mainCamera.audio.Play ();
	}
}
