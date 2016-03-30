using UnityEngine;
using System.Collections.Generic;
using Prime31;


public class IABUIManager : MonoBehaviourGUI
{
#if UNITY_ANDROID

	void Start(){
		var key = "your public key from the Android developer portal here";
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmqxZSaT7hCSfO0nR8GYIisMsh4ALv6lR40rT0u7dUfNUNBT7oHP9/DGSy301NVEhWhBmFVDeSW9vJnp+mfWmQ5WLqrgmEowzBDjrn5xdq65vX/R1E2F9+toRmDQ5/uZt/rVerC/rT1j/T3i0M9m+S5Ny7b86IFxCE+tfnGg618R9ZjaPyZ6bNjNg5OrHb97qvX+qXMD4E1YadHPYg1XOmPBr/53HUGv9PuSA2fgYACU/RTriGS0A1bTldVsvtBzCSgVTZbCokoeLwZ1YFBwjxFI3ab7ICl8qxOePSnpCFpmwwPdAGRNtuqahWNSEAWn4LBFM55jCk2xUJ8CpAFuVmQIDAQAB";
		GoogleIAB.init( key );

		var skus = new string[] { "infinite.lives" };
		GoogleIAB.queryInventory( skus );

	}




	void OnGUI()
	{
	/*	beginColumn();

		if( GUILayout.Button( "Initialize IAB" ) )
		{
			var key = "your public key from the Android developer portal here";
			key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmqxZSaT7hCSfO0nR8GYIisMsh4ALv6lR40rT0u7dUfNUNBT7oHP9/DGSy301NVEhWhBmFVDeSW9vJnp+mfWmQ5WLqrgmEowzBDjrn5xdq65vX/R1E2F9+toRmDQ5/uZt/rVerC/rT1j/T3i0M9m+S5Ny7b86IFxCE+tfnGg618R9ZjaPyZ6bNjNg5OrHb97qvX+qXMD4E1YadHPYg1XOmPBr/53HUGv9PuSA2fgYACU/RTriGS0A1bTldVsvtBzCSgVTZbCokoeLwZ1YFBwjxFI3ab7ICl8qxOePSnpCFpmwwPdAGRNtuqahWNSEAWn4LBFM55jCk2xUJ8CpAFuVmQIDAQAB";
			GoogleIAB.init( key );
		}


		if( GUILayout.Button( "Query Inventory" ) )
		{
			// enter all the available skus from the Play Developer Console in this array so that item information can be fetched for them
			var skus = new string[] { "infinite.lives" };
			GoogleIAB.queryInventory( skus );
		}


		if( GUILayout.Button( "Are subscriptions supported?" ) )
		{
			Debug.Log( "subscriptions supported: " + GoogleIAB.areSubscriptionsSupported() );
		}


		if( GUILayout.Button( "Purchase Test Product" ) )
		{
			GoogleIAB.purchaseProduct( "infinite.lives" );
		}


		if( GUILayout.Button( "Consume Test Purchase" ) )
		{
			GoogleIAB.consumeProduct( "android.test.purchased" );
		}


		if( GUILayout.Button( "Test Unavailable Item" ) )
		{
			GoogleIAB.purchaseProduct( "android.test.item_unavailable" );
		}


		endColumn( true );


		if( GUILayout.Button( "Purchase Real Product" ) )
		{
			GoogleIAB.purchaseProduct( "infinite.lives", "payload that gets stored and returned" );
		}


		if( GUILayout.Button( "Purchase Real Subscription" ) )
		{
			GoogleIAB.purchaseProduct( "com.prime31.testsubscription", "subscription payload" );
		}


		if( GUILayout.Button( "Consume Real Purchase" ) )
		{
			GoogleIAB.consumeProduct( "com.prime31.testproduct" );
		}


		if( GUILayout.Button( "Enable High Details Logs" ) )
		{
			GoogleIAB.enableLogging( true );
		}


		if( GUILayout.Button( "Consume Multiple Purchases" ) )
		{
			var skus = new string[] { "com.prime31.testproduct", "android.test.purchased" };
			GoogleIAB.consumeProducts( skus );
		}

		endColumn();*/
	}
#endif
}
