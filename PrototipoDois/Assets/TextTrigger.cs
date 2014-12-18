using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {

	public GameObject show;
	public GameObject hide;
	public float timer = 0;
	public bool oneTime = false; //mostra a notificaçao so 1 vez
	private int count = 0; //contagem de vezes em que foi chamado

	public bool tutorial = false;
	public int tutorialStep = 0;
	public bool canMove = true;
	public bool canRelease = true;

	public bool changeY = false;
	public float yDirection = 0;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.layer == 9) {
			Debug.Log ("Notification!");

			if (show != null)
				show.SetActive (true);

			if (hide != null)
				hide.SetActive (false);

			Game.balloonController.canMove = canMove;
			Game.balloonController.canRelease = canRelease;

			if (tutorial) {
				Debug.Log ("Tutorial step " + tutorialStep);
			}

			if (oneTime) {
				count++;
				
				if (count > 1)
					DisableShow ();
			}

			if (timer > 0)
				Invoke ("DisableShow", timer);

			if (changeY)
				Game.balloonController.yDirection = yDirection;
		}
	}

	void DisableShow () {
		show.SetActive (false);
	}
}
