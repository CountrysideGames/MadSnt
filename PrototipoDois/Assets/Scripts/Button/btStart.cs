using UnityEngine;
using System.Collections;

public class btStart : MonoBehaviour {

	void Awake () {
		if (Menu.state == 1)
			ShowMenu ();
	}

	void OnMouseDown () {
		ShowMenu ();
	}

	void ShowMenu () {
		Debug.Log ("Showing menu");
		Time.timeScale = 1;
		Menu.state = 1;
		Menu.mainCamera.animation.Play ("menu-down");
	}
}