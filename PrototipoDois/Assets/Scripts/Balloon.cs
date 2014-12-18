using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Balloon : MonoBehaviour {

	public bool menu = false;
	public Transform boxPrefab; //prefab da caixa de presente
	public Transform basket;

	public GameObject[] holes = new GameObject[4]; //LISTA DOS FUROS QUE PODEM SURGIR NO BALAO
	private List<int> openHoles = new List<int>{}; //LISTA DOS FUROS QUE JA FORAM ABERTOS

	public bool canRelease = false; //indica se pode soltar uma caixa
	private int releaseCount = 0; //conta quantos presentes foram soltos

	public int shotCount = 0; //contador de tiros recebidos
	public int giftCount = 0;

	private float currentAcceleration = 0; //aceleraçao atual do acelerometro (para saber o angulo em que o jogador esta segurando o aparelho)

	public float moveSpeed = 3.0f; //velocidade em que o balao se move no eixo X
	private float fallSpeed = 0.6f; //velocidade em que o balao se move no eixo Y
	public float yDirection = 0; //direçao par aonde o balao deve se mover (cima ou baixo)
	public float zRotation = -0.15f; //angulo de rotaçao do balao
	public float fallDelay = 1.0f;

	public bool canMove = false;

	public float upForce = 3.0f; //força que faz o balao subir
	public float downForce = -3.0f; //força que faz o balao descer

	//private float rotationSpeed = 2; //velocidade em que a rotaçao do balao muda



	void Start () {
		//reseta valores estaticos
		shotCount = 0;
		//se nao for tutorial, ja inicia com força para cima
		if (!Game.tutorial) {
			yDirection = upForce;
			canRelease = true;
			canMove = true;
		}
		/*if (menu) {
			canMove = false;
			canRelease = true;
		}*/

		currentAcceleration = Input.acceleration.y; //inclinaçao inicial do acelerometro
	}

	void Update () {
		if (Input.acceleration.y > currentAcceleration || Input.GetKeyDown (KeyCode.W) || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
			if (yDirection != upForce && canMove) {
				if (!animation.IsPlaying ("balloonUp"))
					animation.CrossFade ("balloonUp");
				yDirection = upForce; //muda a direçao do balao para CIMA
				//zRotation = -0.05f; //muda o angulo do balao
			}
		}
		else if (Input.acceleration.y < currentAcceleration || Input.GetKeyDown (KeyCode.S)) {
			if (yDirection != downForce && canMove) {
				if (!animation.IsPlaying ("balloonDown"))
					animation.CrossFade ("balloonDown");
				yDirection = downForce; //muda a direçao do balao para BAIXO
				//zRotation = -0.15f; //muda o angulo do balao
			}
		}

		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began && Input.GetTouch (i).position.y < (Screen.height/3 * 2)) {
				ReleaseBox (); //SOLTA UMA CAIXA
			}
		}

		if (Input.GetKeyDown (KeyCode.Space) || (Input.GetMouseButtonDown(0) && Input.mousePosition.y < (Screen.height/3 * 2))) {
			ReleaseBox (); //SOLTA UMA CAIXA
		}
		
		if (rigidbody2D.position.y < 10.5f) //se o balao estiver abaixo de 7.5
			fallSpeed = Mathf.Lerp (fallSpeed, yDirection, Time.deltaTime * fallDelay); //muda a força do fallSpeed suavemente
		else {//se o balao estiver acima de 12.5
			if (!animation.IsPlaying ("balloonDown"))
				animation.CrossFade ("balloonDown");
			yDirection = downForce; //muda a direçao do balao para BAIXO
			fallSpeed = Mathf.Lerp (fallSpeed, yDirection, Time.deltaTime * fallDelay); //muda a força do fallSpeed suavemente
		}

		if (!menu)
			rigidbody2D.MovePosition (new Vector2 (rigidbody2D.position.x + moveSpeed * Time.deltaTime, rigidbody2D.position.y + fallSpeed * Time.deltaTime));
	}

	void ReleaseBox () {
		if (canRelease) {
			giftCount ++;
			basket.animation.Play ();
			audio.Play ();
			Transform box = Instantiate (boxPrefab) as Transform;
			box.parent = basket;

			if (yDirection == upForce)
				box.localPosition = new Vector2 (.4f, -.7f);
			else
				box.localPosition = new Vector2 (.4f, -1.5f);

			box.parent = null;
			box.rigidbody2D.isKinematic = false;

			if (!Game.manager.menu)
				box.rigidbody2D.AddForce (Vector2.right * 60);
			
			releaseCount ++;
			canRelease = false;
			Invoke ("CanRelease", 0.5f);
		}
	}
	
	void CanRelease () {
		canRelease = true;
	}

	public void OpenHole () {
		int randomHole = Random.Range (0, holes.Length);
		
		if (openHoles.Count < 4)
			while (openHoles.Contains (randomHole))
				randomHole = Random.Range (0, holes.Length);
		
		holes[randomHole].SetActive (true);
		
		openHoles.Add (randomHole);
	}

}