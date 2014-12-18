using UnityEngine;
using System.Collections;

public class btBack : MonoBehaviour {

	void OnMouseDown () {
		ShowMenu ();
	}

	void Update () {
		if (Menu.state == 2 && Input.GetKeyDown (KeyCode.Escape)) {
			ShowMenu ();
		}
	}
	
	void ShowMenu () {
		Debug.Log ("Showing menu");
		Time.timeScale = 1;
		Menu.state = 1;
		Menu.mainCamera.animation.Play ("menu-left");
	}
}
