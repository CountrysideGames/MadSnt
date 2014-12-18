using UnityEngine;
using System.Collections;

public class btPurchase : MonoBehaviour {

	public string productId = "csgms_remove_ads";

	void Awake () {
		if (P31Prefs.getInt ("rads") == 3)
			Destroy (gameObject);
	}

	void OnMouseDown () {
		IAP.purchaseNonconsumableProduct (productId, ( didSucceed, error ) => {
			Debug.Log( "purchasing product " + productId + " result: " + didSucceed );
			
			if( !didSucceed )
				Debug.Log( "purchase error: " + error );
		});
	}
}
