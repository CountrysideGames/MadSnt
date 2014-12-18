using UnityEngine;
using System.Collections;

public class BalloonCollider : MonoBehaviour {


	private int flyHigh = 0;


	void OnTriggerEnter2D (Collider2D col) {

		if (Game.tutorial && col.gameObject.layer == 15) {//TUTORIAL FLOOR//
			Game.balloonController.yDirection = Game.balloonController.upForce;		
			Game.balloon.animation.CrossFade ("balloonUp"); //animaçao do balao subindo

			flyHigh += 1;
			Invoke ("HideWarning", 3.0f);
		}

		if (col.gameObject.layer == 11) {//FAIL//HOUSE//
			Game.balloon.animation.CrossFade ("dead");
			Game.audioManager.Play ("explosion"); //som de explosao
			Game.audioManager.Play ("scream"); //som de grito
			if (!Game.manager.menu)
				Game.manager.fire.animation.Play ("fire");

			rigidbody2D.isKinematic = false; //solta o balao
			Game.balloonController.enabled = false; //desabilita o controle do balao

			GetComponent<CircleCollider2D>().enabled = false;
			Game.fail = true;
			Game.manager.GameOver ();
		}

		if (col.gameObject.layer == 12) {//STAR
			Game.starCount += 1;
			Debug.Log ("Star caught");
			Game.audioManager.Play ("star");
			col.gameObject.animation.Play ("caughtStar"); //destroi a estrela
			Game.manager.CompleteMission (1); //completa a missao 1 (coletar a estrela da fase)
		}

		if (col.gameObject.layer == 13) { //LEVEL END//
			Game.manager.GameOver ();
		}
	}
}
