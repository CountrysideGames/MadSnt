﻿using UnityEngine;
using System.Collections;

public class btExtra : MonoBehaviour {

	void OnMouseDown () {
		ShowExtra ();
	}

	void ShowExtra () {
		Menu.state = 2;
		Debug.Log ("Showing extra");
		Menu.mainCamera.animation.Play ("menu-right");
	}


	void ShowMenu () {
		Menu.state = 1;
		Debug.Log ("Showing menu");
		Menu.mainCamera.animation.Play ("menu-left");
	}
}
