using UnityEngine;
using System.Collections;
using Prime31;

public class btLevel : MonoBehaviour {

	public int levelNumber;
	private bool isLocked = true;
	private SpriteRenderer spriteRenderer;
	private Sprite[] menuSprites;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		menuSprites = Resources.LoadAll<Sprite>("atlas-menu");
	}

	void Start () {
		CheckLock ();
	}

	void OnMouseDown () {
		if (!isLocked) {
			if (levelNumber == 1)
				Game.tutorial = true;
			else
				Game.tutorial = false;
			
			Game.levelNumber = levelNumber;
			Application.LoadLevel ("level" + levelNumber);

#if UNITY_ANDROID
			FlurryAndroid.logEvent( "Level " + levelNumber, false );
#endif
#if UNITY_IOS
			FlurryAnalytics.logEvent( "Level " + levelNumber, false );
#endif
		}
		else
			Debug.Log ("Level " + levelNumber + " is locked!");
	}

	void CheckLock () {
		if (P31Prefs.getInt ("level" + levelNumber) == 1) {//0 TRANCADO - 1 LIBERADO
			Debug.Log ("Level " + levelNumber + " is unlocked!");
			isLocked = false;
			spriteRenderer.color = new Color (1, 1, 1, 0.5f);
			transform.FindChild ("levelNumber").GetComponent<TextMesh>().
				text = levelNumber.ToString();
		}
		else {
			foreach(Sprite s in menuSprites)
			if (s.name.Equals("lock")){
				spriteRenderer.sprite = s;
				break;
			}
			isLocked = true;
		}
	}
}