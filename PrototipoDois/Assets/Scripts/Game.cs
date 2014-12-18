﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	public bool menu = false;
	public int totalChimneys = 10;
	public static int levelNumber = 1;
	public static bool tutorial = false;
	public static bool fail = false; //indica se o player falhou na fase
	public static bool gameOver = false; //indica se o player acabou

	public static int giftCount = 0; //acertos
	public static int shotCount = 0; //erros
	public static int starCount = 0; //estrelas coletadas
	public static int completedMissions = 0; //missoes completas

	public static Game manager; //manager do jogo (referencia estatica desta classe)
	public static GameObject mainCamera; //camera principal do jogo
	public static AudioManager audioManager; //manager dos efeitos sonoros
	public static UIGame uiGame; //manager da interface do jogo
	public static GameObject balloon;
	public static Balloon balloonController;


	private int tutorialStepCount = 0; //step do tutorial
	public GameObject[] tutorialStep = new GameObject[3];
	public GameObject[] tutorialWarning = new GameObject[2];
	
	public GameObject shotLight;
	public GameObject fire;


	void Awake () {
		giftCount = 0;
		shotCount = 0; //zera a contagem de tiros recebidos
		starCount = 0;
		completedMissions = 0; //zera a contagem de missoes completas
		fail = false;
		gameOver = false;

		manager = this;
		mainCamera = GameObject.Find ("MainCamera");
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
		if (!menu) {
			uiGame = GameObject.Find ("EventSystemGame").GetComponent<UIGame>();
			shotLight = GameObject.Find ("shotLight");
			fire = GameObject.Find ("fire");
		}
		balloon = GameObject.Find ("Balloon");
		balloonController = balloon.GetComponent<Balloon> ();

	}

	void Start () {
		if (tutorial) //se o tutorial estiver habilitado, chama o Step 01
			Invoke ("Tutorial01", 1.0f);
	}

	void Tutorial01 () { //MOVIMENTAR O BALAO
		Debug.Log ("Tutorial Step 1");
		tutorialStepCount = 1;
		balloonController.canMove = true; //permita que o balao possa ser movido
		tutorialStep[0].SetActive (true); //mostra a Step 01 do tutorial
		Invoke ("Tutorial02", 7.0f);
	}

	void Tutorial02 () { //JOGAR PRESENTES
		Debug.Log ("Tutorial Step 2");
		tutorialStepCount = 2;
		balloonController.canMove = false; //permita que o balao possa ser movido
		balloonController.canRelease = true; //permita que o balao possa ser movido
		Game.balloonController.yDirection = 0; //mantem o balao andando reto
		tutorialStep[0].SetActive (false); //oculta a Step 01 do tutorial	
		tutorialStep[1].SetActive (true); //mostra a Step 02 do tutorial
	}

	void Update () {

		if (fail) {
			mainCamera.audio.pitch -= 0.1f * Time.deltaTime;
		}

		if (gameOver) {
			mainCamera.audio.volume -= 0.04f * Time.deltaTime;
		}
	}

	public void Tutorial03 () { //ACERTAR CHAMINES
		Debug.Log ("Tutorial Step 3");
		balloonController.canMove = true; //permita que o balao possa ser movido
		tutorialStep[1].SetActive (false); //oculta a Step 01 do tutorial
		tutorialStep[2].SetActive (true); //mostra a Step 02 do tutorial
	}
	public void Tutorial04 () { //ACERTAR CHAMINES
		tutorialStepCount ++;
		Debug.Log ("Tutorial Step 4");
		tutorialStep[2].SetActive (false); //oculta a Step 01 do tutorial
		tutorialStep[3].SetActive (true); //mostra a Step 02 do tutorial
	}
	public void Tutorial05 () { //ACERTAR CHAMINES
		tutorialStepCount ++;
		Debug.Log ("Tutorial Step 5");
		tutorialStep[3].SetActive (false); //oculta a Step 01 do tutorial
		tutorialStep[4].SetActive (true); //mostra a Step 02 do tutorial
	}

	public void GameOver () {
		Debug.Log ("Game Over");
		balloonController.canMove = false;
		balloonController.canRelease = false;
		balloonController.yDirection = 0;
		gameOver = true;
		Invoke ("ShowGameOver", 2.0f);

		//CONFERE MISSOES
		if (giftCount >= totalChimneys)
			CompleteMission (0);
		if (shotCount <= 0) //MISSAO DE NAO TOMAR TIROS
			CompleteMission (2);

		if (!menu) {
			for (int i = 0; i < 3; i++) {
				if (P31Prefs.getInt ("level" + levelNumber + "mission" + i) == 1) {
					uiGame.obCheck [i].GetComponent<Image> ().sprite = uiGame.obChecked;
					completedMissions += 1;
					Debug.Log ("Completed mission " + i);
				}
			}
		}

		//LIBERA O PROXIMO LEVEL
		if (Game.completedMissions >= 2) {
			P31Prefs.setInt ("level" + Game.levelNumber, 1);
		}

		Debug.Log ("Gifts delivered: " + giftCount + ", Stars collected " + starCount + ", shots received " + shotCount);
	}
	void ShowGameOver () {
		mainCamera.audio.loop = false;
		if (!menu)
			uiGame.panelGameOver.SetActive (true); //mostra o painel de GameOver
	}

	//COMPLETA UMA DAS 3 MISSOES:
	//0- ACERTAR TODAS AS CHAMINES
		//1- COLETAR UMA ESTRELA
	//2- NAO TOMAR TIROS
	public void CompleteMission (int missionNumber) {
		P31Prefs.setInt ("level" + levelNumber + "mission" + missionNumber, 1); //0 = INCOMPLETO, 1 = COMPLETO
	}
}