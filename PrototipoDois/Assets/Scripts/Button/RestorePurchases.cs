using UnityEngine;
using System.Collections;

public class RestorePurchases : MonoBehaviour {



	void OnMouseDown () {
#if UNITY_ANDROID
		var skus = new string[] { "csgms_remove_ads" };
		GoogleIAB.queryInventory( skus );
#endif
#if UNITY_IOS
		IAP.restoreCompletedTransactions( productId =>
		                                 {
			Debug.Log( "restored purchased product: " + productId );
		});
#endif
	}

}
