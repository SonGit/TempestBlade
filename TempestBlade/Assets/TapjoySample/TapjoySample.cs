using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

public class TapjoySample : MonoBehaviour, ITapjoyEvent
{
	private static string ENABLE_LOGGING_IOS = "TJC_OPTION_ENABLE_LOGGING";
	private static string ENABLE_LOGGING_ANDROID = "enable_logging";
	string tapPointsLabel = "";
	bool viewIsShowing = false;
	bool shouldTransition = false;
	TapjoyEvent diectPlayEvent;
	TapjoyEvent sampleEvent;
	
	void Start ()
	{
#if UNITY_ANDROID
		// Attach our thread to the java vm; obviously the main thread is already attached but this is good practice..
		if (Application.platform == RuntimePlatform.Android)
			UnityEngine.AndroidJNI.AttachCurrentThread();
#endif
	
		Dictionary<String, System.Object> connectFlags = new Dictionary<String, System.Object>();
		
		// Connect to the Tapjoy servers.
		if (Application.platform == RuntimePlatform.Android)
        {
			// Enable logging
			connectFlags.Add(ENABLE_LOGGING_ANDROID, true);

			// If you are not using Tapjoy Managed currency, you would set your own user ID here.
			//  connectFlags.Add("user_id", "A_UNIQUE_USER_ID");

			// You can also set your event segmentation parameters here.
			//  Dictionary<String, System.Object> segmentationParams = new Dictionary<String, System.Object>();
			//  segmentationParams.Add("iap", true);
			//  connectFlags.Add("segmentation_params", segmentationParams);

			TapjoyPlugin.RequestTapjoyConnect("bba49f11-b87f-4c0f-9632-21aa810dd6f1",			// YOUR APP ID GOES HERE
                                               "yiQIURFEeKm0zbOggubu",							// YOUR SECRET KEY GOES HERE
                                               connectFlags);                					// YOUR CONNECT FLAGS
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
			
			// Enable logging
			connectFlags.Add(ENABLE_LOGGING_IOS, true);
			
			// Add other connect flags
			connectFlags.Add("TJC_OPTION_COLLECT_MAC_ADDRESS", TapjoyPlugin.MacAddressOptionOffWithVersionCheck);

			// If you are not using Tapjoy Managed currency, you would set your own user ID here.
		  	// connectFlags.Add("TJC_OPTION_USER_ID", "A_UNIQUE_USER_ID");
		
			// You can also set your event segmentation parameters here.
			//  Dictionary<String, System.Object> segmentationParams = new Dictionary<String, System.Object>();
			//  segmentationParams.Add("iap", true);
			//  connectFlags.Add("TJC_OPTION_SEGMENTATION_PARAMS", segmentationParams);
			
            
			TapjoyPlugin.RequestTapjoyConnect("13b0ae6a-8516-4405-9dcf-fe4e526486b2",			// YOUR APP ID GOES HERE
													"XHdOwPa8de7p4aseeYP0",						// YOUR SECRET KEY GOES HERE
													connectFlags);								// YOUR CONNECT FLAGS
        }

	}
	
	void Awake()
	{
		Debug.Log("C#: Awaking and adding Tapjoy Events");

		// Tapjoy Connect Events
		TapjoyPlugin.connectCallSucceeded += HandleTapjoyConnectSuccess;
		TapjoyPlugin.connectCallFailed += HandleTapjoyConnectFailed;
		
		// Tapjoy Virtual Currency Events
		TapjoyPlugin.getTapPointsSucceeded += HandleGetTapPointsSucceeded;
		TapjoyPlugin.getTapPointsFailed += HandleGetTapPointsFailed;
		TapjoyPlugin.spendTapPointsSucceeded += HandleSpendTapPointsSucceeded;
		TapjoyPlugin.spendTapPointsFailed += HandleSpendTapPointsFailed;
		TapjoyPlugin.awardTapPointsSucceeded += HandleAwardTapPointsSucceeded;
		TapjoyPlugin.awardTapPointsFailed += HandleAwardTapPointsFailed;
		TapjoyPlugin.tapPointsEarned += HandleTapPointsEarned;
		
		// Tapjoy Full Screen Ad Events
		TapjoyPlugin.getFullScreenAdSucceeded += HandleGetFullScreenAdSucceeded;
		TapjoyPlugin.getFullScreenAdFailed += HandleGetFullScreenAdFailed;
		
		// Tapjoy Video Ad Events
		TapjoyPlugin.videoAdStarted += HandleVideoAdStarted;
		TapjoyPlugin.videoAdFailed += HandleVideoAdFailed;
		TapjoyPlugin.videoAdCompleted += HandleVideoAdCompleted;
		
		// Tapjoy Ad View Closed Events
		TapjoyPlugin.viewOpened += HandleViewOpened;
		TapjoyPlugin.viewClosed += HandleViewClosed;
 	
		// Tapjoy Show Offers Events
		TapjoyPlugin.showOffersFailed += HandleShowOffersFailed;
	}

	void OnApplicationPause(bool pause)
	{
		Debug.Log("C#: Application Pause: " + pause + " expected transition: " + shouldTransition);
		if (!shouldTransition) {
			if (pause)
			{
				TapjoyPlugin.AppPause();
			}
			else
			{
				TapjoyPlugin.AppResume();
			}
		}

	}
	
	void OnDisable()
	{
		Debug.Log("C#: Disabling and removing Tapjoy Events");
		// Tapjoy Connect Events
		TapjoyPlugin.connectCallSucceeded -= HandleTapjoyConnectSuccess;
		TapjoyPlugin.connectCallFailed -= HandleTapjoyConnectFailed;
		
		// Tapjoy Virtual Currency Events
		TapjoyPlugin.getTapPointsSucceeded -= HandleGetTapPointsSucceeded;
		TapjoyPlugin.getTapPointsFailed -= HandleGetTapPointsFailed;
		TapjoyPlugin.spendTapPointsSucceeded -= HandleSpendTapPointsSucceeded;
		TapjoyPlugin.spendTapPointsFailed -= HandleSpendTapPointsFailed;
		TapjoyPlugin.awardTapPointsSucceeded -= HandleAwardTapPointsSucceeded;
		TapjoyPlugin.awardTapPointsFailed -= HandleAwardTapPointsFailed;
		TapjoyPlugin.tapPointsEarned -= HandleTapPointsEarned;
		
		// Tapjoy Full Screen Ad Events
		TapjoyPlugin.getFullScreenAdSucceeded -= HandleGetFullScreenAdSucceeded;
		TapjoyPlugin.getFullScreenAdFailed -= HandleGetFullScreenAdFailed;
		
		// Tapjoy Video Ad Events
		TapjoyPlugin.videoAdStarted -= HandleVideoAdStarted;
		TapjoyPlugin.videoAdFailed -= HandleVideoAdFailed;
		TapjoyPlugin.videoAdCompleted -= HandleVideoAdCompleted;
		
		// Tapjoy Ad View Closed Events
		TapjoyPlugin.viewOpened -= HandleViewOpened;
		TapjoyPlugin.viewClosed -= HandleViewClosed;
		
		// Tapjoy Show Offers Events
		TapjoyPlugin.showOffersFailed -= HandleShowOffersFailed;
	}

	
	#region Tapjoy Callback Methods (These must be implemented in your own c# script file.)

	// CONNECT
	public void HandleTapjoyConnectSuccess()
	{
		Debug.Log("C#: HandleTapjoyConnectSuccess");

		
		// Get the user virtual currency
		TapjoyPlugin.GetTapPoints();

		// Preload direct play event
		diectPlayEvent = new TapjoyEvent("video_unit", this);
		diectPlayEvent.EnablePreload(true);
		diectPlayEvent.Send();
	}
	
	public void HandleTapjoyConnectFailed()
	{
		Debug.Log("C#: HandleTapjoyConnectFailed");
	}
	
	// VIRTUAL CURRENCY
	void HandleGetTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleGetTapPointsSucceeded: " + points);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}
	
	public void HandleGetTapPointsFailed()
	{
		Debug.Log("C#: HandleGetTapPointsFailed");
	}
	
	public void HandleSpendTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleSpendTapPointsSucceeded: " + points);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void HandleSpendTapPointsFailed()
	{
		Debug.Log("C#: HandleSpendTapPointsFailed");
	}

	public void HandleAwardTapPointsSucceeded()
	{
		Debug.Log("C#: HandleAwardTapPointsSucceeded");
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}

	public void HandleAwardTapPointsFailed()
	{
		Debug.Log("C#: HandleAwardTapPointsFailed");
	}
	
	public void HandleTapPointsEarned(int points)
	{
		Debug.Log("C#: CurrencyEarned: " + points);
		tapPointsLabel = "Currency Earned: " + points;
		
		TapjoyPlugin.ShowDefaultEarnedCurrencyAlert();
	}
	
	// FULL SCREEN ADS
	public void HandleGetFullScreenAdSucceeded()
	{
		Debug.Log("C#: HandleGetFullScreenAdSucceeded");
		
		TapjoyPlugin.ShowFullScreenAd();
	}
	
	public void HandleGetFullScreenAdFailed()
	{
		Debug.Log("C#: HandleGetFullScreenAdFailed");
	}
	
	// VIDEO
	public void HandleVideoAdStarted()
	{
		Debug.Log("C#: HandleVideoAdStarted");
	}
	
	public void HandleVideoAdFailed()
	{
		Debug.Log("C#: HandleVideoAdFailed");
	}
	
	public void HandleVideoAdCompleted()
	{
		Debug.Log("C#: HandleVideoAdCompleted");
	}
	
	// VIEW OPENED	
	public void HandleViewOpened(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewOpened");
		viewIsShowing = true;
	}
	
	// VIEW CLOSED	
	public void HandleViewClosed(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewClosed");
		viewIsShowing = false;
	}
	
	// OFFERS
	public void HandleShowOffersFailed()
	{
		Debug.Log("C#: HandleShowOffersFailed");
	}
	
	#endregion
	
	#region ITapjoyEvent callback methods

	public void SendEventSucceeded(TapjoyEvent tapjoyEvent, bool contentIsAvailable)
	{
		Debug.Log("C#: SendEventSucceeded, contentIsAvailable: " + contentIsAvailable + " for event: " + tapjoyEvent.GetName());
	}

	public void ContentIsReady(TapjoyEvent tapjoyEvent, int status)
	{
		Debug.Log("C#: ContentIsReady for event: " + tapjoyEvent.GetName() + " with status: " + status);

		/*
		switch(status) {
			case TJCEventPreloadPartial:
 				// handle partial load of cache
 				break;
 			case TJCEventPreloadComplete:
 				// handle complete load of cache
				break;
		}
		*/
	}

	public void SendEventFailed(TapjoyEvent tapjoyEvent, string error)
	{
		Debug.Log("C#: SendEventFailed for event: " + tapjoyEvent.GetName() + " with error: " + error);
	}

	public void ContentDidAppear(TapjoyEvent tapjoyEvent)
	{
		Debug.Log("C#: ContentDidAppear for event: " + tapjoyEvent.GetName());
	}

	public void ContentDidDisappear(TapjoyEvent tapjoyEvent)
	{
		Debug.Log("C#: ContentDidDisappear for event: " + tapjoyEvent.GetName());

		// Pre-load next event if direct play event
		if (tapjoyEvent.GetName() == diectPlayEvent.GetName()) {
			diectPlayEvent = new TapjoyEvent("video_unit", this);
			diectPlayEvent.EnablePreload(true);
			diectPlayEvent.Send();
			tapPointsLabel = "Loading next direct play event.";
		}
	}

	public void DidRequestAction(TapjoyEvent tapjoyEvent, TapjoyEventRequest request)
	{
		Debug.Log("C#: DidRequestAction type:" + request.type + ", identifier:" + request.identifier + ", quantity:" + request.quantity);

		/*
		// Your app should perform an action based on the value of the request.type property
		switch(request.type){
			case TapjoyEventRequest.TYPE_IN_APP_PURCHASE:
				// Your app should initiate an in-app purchase of the product identified by request.identifier
				break;
			case TapjoyEventRequest.TYPE_VIRTUAL_GOOD:
				// Your app should award the user the item specified by request.identifier with the amount specified by request.quantity
				break;
			case TapjoyEventRequest.TYPE_CURRENCY:
				// The user has been awarded the currency specified with request.identifier, with the amount specified by request.quantity
				break;
			case TapjoyEventRequest.TYPE_NAVIGATION:
				// Your app should attempt to navigate to the location specified by request.identifier
				break;
		}
		*/

		// Your app must call either EventRequestCompleted() or EventRequestCancelled() to complete the lifecycle of the request
		request.EventRequestCompleted();
	}

	#endregion
	
	#region GUI for sample app
	
	public void ResetTapPointsLabel()
	{
		tapPointsLabel = "Updating Tap Points...";
	}
	
	void OnGUI()
	{
		if (viewIsShowing)
			return;
		
		GUIStyle labelStyle = new GUIStyle();
		labelStyle.alignment = TextAnchor.MiddleCenter;
		labelStyle.normal.textColor = Color.white;
		labelStyle.wordWrap = true;
		
		float centerx = Screen.width / 2;
		//float centery = Screen.height / 2;
		float spaceSize = 60;
		float buttonWidth = 300;
		float buttonHeight = 50;
		float fontSize = 20;
		float spacer = 100;
		
		// Quit app on BACK key.
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
		
		GUI.Label(new Rect(centerx - 200, spacer, 400, 25), "Tapjoy Connect Sample App", labelStyle);
		
		spacer += fontSize + 10;
		
		if (GUI.Button(new Rect(centerx - (buttonWidth / 2), spacer, buttonWidth, buttonHeight), "Show Offers"))
		{
			shouldTransition = true;
			TapjoyPlugin.ShowOffers();
		}
		
		spacer += spaceSize;
		
		if (GUI.Button(new Rect(centerx - (buttonWidth / 2), spacer, buttonWidth, buttonHeight), "Show Direct Play Video Ad"))
		{
			if (diectPlayEvent.IsContentAvailable())
			{
				if (diectPlayEvent.IsContentReady())
				{
					shouldTransition = true;
					diectPlayEvent.Show();
				} 
				else
				{
					tapPointsLabel = "Direct play video not ready to show.";
				}
			}
			else
			{
				tapPointsLabel = "No direct play video to show.";
			}
		}
		
		spacer += spaceSize;
		
		if (GUI.Button(new Rect(centerx - (buttonWidth / 2), spacer, buttonWidth, buttonHeight), "Get Tap Points"))
		{
			TapjoyPlugin.GetTapPoints();
			ResetTapPointsLabel();
		}
		
		spacer += spaceSize;
		
		if (GUI.Button(new Rect(centerx - (buttonWidth / 2), spacer, buttonWidth, buttonHeight), "Spend Tap Points"))
		{
			TapjoyPlugin.SpendTapPoints(10);
			ResetTapPointsLabel();
		}
		
		spacer += spaceSize;
		
		if (GUI.Button(new Rect(centerx - (buttonWidth / 2), spacer, buttonWidth, buttonHeight), "Award Tap Points"))
		{
			TapjoyPlugin.AwardTapPoints(10);
			ResetTapPointsLabel();
		}
		
		spacer += spaceSize;
		
		if (GUI.Button(new Rect(centerx - (buttonWidth / 2), spacer, buttonWidth, buttonHeight), "Send Event"))
		{
			// Create a new sample event
			sampleEvent = new TapjoyEvent("test_unit", this);
			if (sampleEvent != null)
			{
				sampleEvent.EnableAutoPresent(true);
				shouldTransition = true;
				sampleEvent.Send();
			}
		}

		spacer += fontSize;
		
		// Display status
		GUI.Label(new Rect(centerx - 200, spacer, 400, 150), tapPointsLabel, labelStyle);
	}
	
	#endregion
}