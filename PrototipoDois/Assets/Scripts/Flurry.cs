using UnityEngine;
using System.Collections;

public class Flurry : MonoBehaviour {

	//INICIA O FLURRY PARA COLETA DE DADOS DE USO

	public bool testAds = false;


	void Start () {
#if UNITY_ANDROID
		FlurryAndroid.onStartSession( "JTR3SGZZ9C2439G956Q7", true, testAds );
#endif
#if UNITY_IOS
		FlurryAnalytics.startSession( "H8QFPTXMM9NH2W3QVWX4" );
#endif
	}
}
