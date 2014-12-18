using UnityEngine;
using System.Collections;

public class ButtonLink : MonoBehaviour {

	public string androidURL;
	public string iphoneURL;
	public string windowsphoneURL;
	public string blackberryURL;

	void OnMouseDown () {
		var url = "";

#if UNITY_ANDROID
		url = androidURL;
#endif
#if UNITY_IOS
		url = iphoneURL;
#endif
#if UNITY_WP8
		url = windowsphoneURL;
#endif
#if UNITY_BLACKBERRY
		url = blackberryURL;
#endif

		Application.OpenURL (url);
	}

}
