using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class IAPManager : MonoBehaviour {
	
	void Awake () {
#if UNITY_ANDROID
		var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAibNcF/DRnzIBlPWzp3lNR3zZYH7Z/b4k8gBZgG3XWLJ/eYTKcUK1vV5hbUx71vO+d4k5eAaOudhD6OlTxi7agex3qaNu45CDBMkS16KzUJBttDzv8WDi7qsbTDBqADXIOpJ5gTWjgTzENgX5RixlZDDlLwX47sjKvxVJgL2oEzjl0F+xquAWY22QuSCFzJzRopxh53V6RCf+SdOtFk2U/uRdoo2VG0R87Fh/3BsVcmp4PUolRKr+jf02/vhm/zhZ8RpzYgJ7kN+qlUXN0ujgJAOFiJbdErGVb45TLFuRN5ybofHa327U5NeOxCasyIhk8tZNGcYvehWaKLtbv42DMQIDAQAB";
		IAP.init( key );
#endif
	}
	void Start () {
		var androidSkus = new string[] { "csgms_remove_ads" };
		var iosProductIds = new string[] { "csgms_remove_ads" };
		
		IAP.requestProductData( iosProductIds, androidSkus, productList =>
		                       {
			Debug.Log( "Product list received" );
			Utils.logObject( productList );
		});
	}

#if UNITY_ANDROID
	void OnEnable () {
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
	}
	
	void OnDisable () {
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
	}

	void queryInventorySucceededEvent (List<GooglePurchase> purchases, List<GoogleSkuInfo> skus) {
		foreach (GooglePurchase product in purchases) //PEGA TODAS AS COMPRAS DA LISTA DE COMPRAS
			RetrieveProduct (product.productId); //E RESTAURA
	}
	void purchaseSucceededEvent (GooglePurchase purchase) {
		RetrieveProduct (purchase.productId); //ENTREGA COMPRA DO PRODUTO
	}
#endif
	public void RetrieveProduct (string productId) {
		if (productId == "csgms_remove_ads") {
			P31Prefs.setInt ("rads", 3); //quando RADS vira 3, ele oculta os ads
			Ads.Refresh ();
		}
	}
}
