using UnityEngine;
using System.Collections;

public class UIGame : MonoBehaviour {

	public GameObject panelPause;
	public GameObject panelGameOver;
	public GameObject[] obCheck = new GameObject[3];
	public Sprite obChecked;

	void Start () {
		Time.timeScale = 1;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Pause ();
	}

	public void Pause () {
		Debug.Log ("Pause");
		if (!panelPause.activeSelf) {
			Time.timeScale = 0;
			Game.audioManager.Pause ("theme");
			Game.balloonController.enabled = false;
			panelPause.SetActive (true);
		}
		else {
			Time.timeScale = 1;
			Game.audioManager.Play ("theme");
			Game.balloonController.enabled = true;
			panelPause.SetActive (false);
		}
	}

	public void GoToMenu () { //ABRE O MENU PRINCIPAL
		Application.LoadLevel ("menu");
	}

	public void Restart () { //REINICIA A FASE ATUAL
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void NextLevel () { //INICIA A PROXIMA FASE
		if (Game.completedMissions >= 2) {
			Debug.Log ("Skipping to next level");
			Game.tutorial = false;
			Game.levelNumber += 1;
			P31Prefs.setInt ("level" + Game.levelNumber, 1);
			Application.LoadLevel ("level" + Game.levelNumber);
		}
		else {
			var buttons = new string[] { "OK" };
#if UNITY_ANDROID
			EtceteraAndroid.showAlert( "Hey!", "You need 2 stars to unlock the next level", buttons[0] );
#endif
#if UNITY_IOS
			EtceteraBinding.showAlertWithTitleMessageAndButtons( "Hey!", "You need 2 stars to unlock the next level", buttons );
#endif
			Debug.Log ("Need more stars to unlock the next level");
		}
	}
}
