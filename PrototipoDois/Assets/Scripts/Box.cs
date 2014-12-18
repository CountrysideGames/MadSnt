using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box : MonoBehaviour {

	bool canCollide = true;

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.layer == 10) {//CHIMNEY / CHAMINE//
			if (Game.balloonController.moveSpeed < 5.0f) {//acelera o balao//
				Game.balloonController.moveSpeed += .1f;
				Game.balloonController.fallDelay += .05f;
				Game.balloonController.upForce += .1f;
				Game.balloonController.downForce += .1f;
				Game.balloonController.fallDelay += .1f;
				col.animation.Play ("chimneyHit");
			}
			Game.giftCount += 1;

			Game.audioManager.Play ("success");
			
			Game.audioManager.Play ("nice"); //toca som de sucesso
			Invoke ("DestroyBox", .1f);
			canCollide = false;
		}
			
		if (col.gameObject.layer == 11) {//FAIL//
			Debug.Log ("Wrong hit");
			
			var randomAudio = Random.Range (0, 10);
			if (randomAudio > 8)
				Game.audioManager.Play ("angry");

			if (canCollide && !Game.tutorial) { //se nao estiver no tutorial)
				Invoke ("GetShot", 1.5f);
				canCollide = false;
			}

			if (col.name == "tree")
				Game.audioManager.Play ("tree");
			if (col.name == "Floor")
				Game.audioManager.Play ("floor");
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (canCollide) {
			if (col.gameObject.layer == 11) {//FAIL//
				Game.audioManager.Play ("roof");

				var randomAudio = Random.Range (0, 10);
				if (randomAudio > 8)
					Game.audioManager.Play ("angry");
			
				if (!Game.tutorial) { //se nao estiver no tutorial
					Invoke ("GetShot", 1.5f);
					canCollide = false;
				}
			}
		}
	}

	void DestroyBox () {
		Destroy (gameObject);
	}

	void GetShot () {
		if (!Game.manager.menu) {
			Game.mainCamera.audio.pitch += 0.05f;
			Game.mainCamera.animation.Play ("cameraHit");
			Game.manager.shotLight.animation.Play ("shotLight");
		}
		Game.shotCount ++;
		Game.balloonController.OpenHole (); //mostra um furo
		Game.audioManager.Play ("shot"); //som de tiro
		Game.audioManager.Play ("air"); //som de ar
		Game.balloonController.upForce -= 0.5f;
		Game.balloonController.downForce -= 0.5f;
		Game.balloonController.moveSpeed += 0.5f;

		if (Game.shotCount > 2) {
			Game.balloon.rigidbody2D.isKinematic = false; //solta o balao
			Game.balloonController.enabled = false; //desabilita o controle do balao
			if (Game.manager.menu)
				Game.balloon.animation.CrossFade ("dead");
			Debug.Log ("Game Over");
		}
	}
}