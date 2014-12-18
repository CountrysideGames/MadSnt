using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class Ads : MonoBehaviour {

	public bool onClick = false;
	public bool onEnable = false;
	public bool show = true;
	public bool hide = false;
	public bool banner = false;
	public AdMobLocation AdMobBannerLocation = AdMobLocation.BottomCenter;
#if UNITY_ANDROID
	public FlurryAdPlacement FlurryBannerLocation = FlurryAdPlacement.BannerBottom;
#endif
#if UNITY_IOS
	public FlurryAdSize FlurryBannerLocation = FlurryAdSize.Bottom;
#endif
	public bool interstitial = false;
	public int chance = 10; //chance de um Ad aparecer
	public string location = null;
	


	void Awake () {
		Chartboost.init ("5491d1220d6025231a23cfda", "3be619ad7ab8d1fd083fbb36cd91cb7a12c3dca2", "5491d1220d6025231a23cfda", "3be619ad7ab8d1fd083fbb36cd91cb7a12c3dca2buy", true);

		AdBuddizBinding.SetAndroidPublisherKey("3af7f480-c341-42c0-a7e7-5375f984acb0"); //chave do Android
		AdBuddizBinding.SetIOSPublisherKey("201a0277-0e68-47d2-9cc0-f39f6cb56e30"); //chave do iOS
	}

	void OnEnable () {
		if (onEnable)
			Ad ();

		AdMob.failedToReceiveAdEvent += failedToReceiveAdMob;
		Chartboost.didFailToCacheInterstitialEvent += didFailToCacheChartboost;
	}
	
	void OnDisable () {
		AdMob.failedToReceiveAdEvent -= failedToReceiveAdMob;
	}

	void OnMouseDown () {
		if (onClick)
			Ad ();
	}

	public static void Refresh () {
		if (P31Prefs.getInt ("rads") == 3) {
			AdMob.destroyBanner ();
#if UNITY_ANDROID
			FlurryAndroid.removeAd( "main_menu" ); //oculta banner do Flurry
#endif
#if UNITY_IOS
			FlurryAds.removeAdFromSpace( "main_menu" );
#endif
		}
	}

	public void ShowInterstitial (string local) {
		Chartboost.showInterstitial (local);
	}

	void Ad () {
		int randomNumber = Random.Range (0, 10);
		
		if (hide) {
			AdMob.destroyBanner (); //oculta banner do AdMob
#if UNITY_ANDROID
			FlurryAndroid.removeAd( "main_menu" ); //oculta banner do Flurry
#endif
#if UNITY_IOS
			FlurryAds.removeAdFromSpace( "main_menu" ); //oculta banner do Flurry
#endif
		}

		if (randomNumber <= chance) {
			if (banner == false && interstitial == false)
				banner = true;

			if (show && P31Prefs.getInt ("rads") == 0) {
				if (banner) {
					AdMob.createBanner ("ca-app-pub-8470511340989148/5727361119", "ca-app-pub-8470511340989148/7343695112", AdMobBanner.SmartBanner, AdMobBannerLocation);
				}
				if (interstitial)
					ShowInterstitial (location);
			}
		}
	}


//EVENTOS//		
	void failedToReceiveAdMob (string error) {//SE ADMOB FALHAR
		Debug.Log ("AdMob falhou: " + error); 

#if UNITY_ANDROID
		FlurryAndroid.displayAd( "space", FlurryBannerLocation, 1000 ); //MOSTRA ADS DO FLURRY
#endif
#if UNITY_IOS
		FlurryAds.displayAdForSpace( "space", FlurryBannerLocation );
#endif
	}

	void didFailToCacheChartboost (string location, string error) {//SE CHARTBOOST FALHAR
		AdBuddizBinding.ShowAd(); //mostra AdBuddiz
	}
}