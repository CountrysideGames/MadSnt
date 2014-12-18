using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public static GameObject mainCamera;
	public static int state = 0;

	void Awake () {
		P31Prefs.setInt ("level1", 1);
		mainCamera = this.gameObject;

#if UNITY_ANDROID
		EtceteraAndroid.askForReview (1, 1, 2, "Do you like this game?", "Please review the game if you do!" , false);
#endif
#if UNITY_IOS
		EtceteraBinding.askForReview(1, 0.5f, "Do you like this game?", "Please review the game if you do!", "950049277" );
#endif
	}
}