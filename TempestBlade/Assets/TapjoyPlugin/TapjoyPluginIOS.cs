#if UNITY_IPHONE
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

// Same enum found in Tapjoy SDK, placed here for convenience.
public enum TapjoyTransition
{
	TJCTransitionBottomToTop = 0,		/*!< View animates from the bottom to the top of the screen. */
	TJCTransitionTopToBottom,			/*!< View animates from the top to the bottom of the screen. */
	TJCTransitionLeftToRight,			/*!< View animates from the left to the right of the screen. */
	TJCTransitionRightToLeft,			/*!< View animates from the right to the left of the screen. */
	TJCTransitionFadeEffect,			/*!< View fades into visibility. */
	TJCTransitionNormalToBottom,		/*!< View animates off screen downwards. */
	TJCTransitionNormalToTop,			/*!< View animates off screen upwards. */
	TJCTransitionNormalToLeft,			/*!< View animates off screen to the left. */
	TJCTransitionNormalToRight,			/*!< View animates off screen to the right. */
	TJCTransitionFadeEffectReverse,		/*!< View fades out of visibility. */
	TJCTransitionExpand,					/*!< View starts in the middle with zero size and expands to fit the screen. */
	TJCTransitionShrink,					/*!< View shrinks to the middle of the screen until no visible. */
	TJCTransitionFlip,		
	TJCTransitionFlipReverse,
	TJCTransitionPageCurl,
	TJCTransitionPageCurlReverse,
	TJCTransitionNoEffect = -1			/*!< No animation effect. */
}


public class TapjoyPluginIOS : MonoBehaviour
{
	private static string CONNECT_FLAG_KEY = "connectFlags";
	private static string SEGMENTS_DICTIONARY_KEY = "segmentsDictionary";
	
	#region	Interface to native implementation
	
	[DllImport("__Internal")]
	extern static public void _SetCallbackHandler(string handlerName);
	
	[DllImport ("__Internal")]
	private static extern void _RequestTapjoyConnect(string appID, string secretKey);
	
	[DllImport ("__Internal")]
	private static extern void _SetKeyToValueInDictionary(string key, string valueToSet, string dictionaryName);

	[DllImport ("__Internal")]
	private static extern void _SetKeyToDictionaryRefValueInDictionary(string key, string dictionaryNameToSet, string dictionaryNameToSetTo);
	
	[DllImport ("__Internal")]
	private static extern void _EnableLogging(bool enable);

	[DllImport ("__Internal")]
	private static extern void _SendSegmentationParams(String dictionaryNameOfSegments);

	[DllImport ("__Internal")]
	private static extern void _ActionComplete(string actionID);
	
	[DllImport ("__Internal")]
	private static extern void _SetUserID(string userID);
	
	[DllImport ("__Internal")]
	private static extern void _ShowOffers();
	
	[DllImport ("__Internal")]
	private static extern void _ShowOffersWithCurrencyID(string currencyID, bool isSelectorVisible);
	
	[DllImport ("__Internal")]
	private static extern void _GetFullScreenAd();
	
	[DllImport ("__Internal")]
	private static extern void _GetFullScreenAdWithCurrencyID(string currencyID);
	
	[DllImport ("__Internal")]
	private static extern void _SetCurrencyMultiplier(float multiplier);
	
	[DllImport ("__Internal")]	
	private static extern void _ShowFullScreenAd();
	
	[DllImport ("__Internal")]
	private static extern void _ShowDefaultEarnedCurrencyAlert();
	
	[DllImport ("__Internal")]
	private static extern void _GetTapPoints();

	[DllImport ("__Internal")]
	private static extern void _SpendTapPoints(int points);
	
	[DllImport ("__Internal")]
	private static extern void _AwardTapPoints(int points);
	
	[DllImport ("__Internal")]
	private static extern int _QueryTapPoints();
	
	[DllImport ("__Internal")]
	private static extern void _GetDisplayAd();
	
	[DllImport ("__Internal")]
	private static extern void _ShowDisplayAd();
	
	[DllImport ("__Internal")]
	private static extern void _HideDisplayAd();
	
	[DllImport ("__Internal")]
	private static extern void _GetDisplayAdWithCurrencyID(string currencyID);
	
	[DllImport ("__Internal")]
	private static extern void _SetDisplayAdSize(string size);
	
	[DllImport ("__Internal")]
	private static extern void _EnableDisplayAdAutoRefresh(bool enable);
	
	[DllImport ("__Internal")]
	private static extern void _MoveDisplayAd(int x, int y);
	
	[DllImport ("__Internal")]
	private static extern void _SetTransitionEffect(int transition);
	
	[DllImport ("__Internal")]
	private static extern void _SendIAPEvent(string name, float price, int quantity, string currencyCode);	
	
	[DllImport ("__Internal")]
	private static extern void _CreateEvent(string eventGuid, string eventName, string eventParameter);
	
	[DllImport ("__Internal")]
	private static extern void _SendEvent(string guid);
	
	[DllImport ("__Internal")]
	private static extern void _ShowEvent(string guid);
	
	[DllImport ("__Internal")]
	private static extern void _EnableEventAutoPresent(string guid, bool autoPresent);
	
	[DllImport ("__Internal")]
	private static extern void _EnableEventPreload(string guid, bool preload);

	[DllImport ("__Internal")]
	private static extern void _EventRequestCompleted(string guid);

	[DllImport ("__Internal")]
	private static extern void _EventRequestCancelled(string guid);

	#endregion
	
	#region Declarations for non-native
	
	public static void SetCallbackHandler(string handlerName)
	{
		_SetCallbackHandler(handlerName);
	}
	
	public static void RequestTapjoyConnect(string appID, string secretKey)
	{
		_RequestTapjoyConnect(appID, secretKey);
	}
	public static void RequestTapjoyConnect(string appID, string secretKey, Dictionary<string, System.Object> flags)
	{
		if (flags != null)
		{
			foreach (KeyValuePair<string, System.Object> kvp in flags)
			{
				// Is the value a dictionary itself
				if (kvp.Value.GetType().IsGenericType)
				{
					Dictionary<string, System.Object> dictionaryToTransfer = (Dictionary<string, System.Object>) kvp.Value;
					string dictionaryName = kvp.Key;
					// Communicate with objetive-c to build the dictionary
					transferDictionaryToObjectiveCWithName(dictionaryToTransfer, dictionaryName);
					// Tell Objective-C to add the dictionary *dictionaryName* to dictionary "connectFlags" under the key *kvp.key* 
					_SetKeyToDictionaryRefValueInDictionary(kvp.Key, dictionaryName, TapjoyPluginIOS.CONNECT_FLAG_KEY);
				} else {
					// Tell Objective-C to add the value *kvp.Value* to dictionary "connectFlags" under the key *kvp.Key*
					_SetKeyToValueInDictionary(kvp.Key, kvp.Value.ToString(), TapjoyPluginIOS.CONNECT_FLAG_KEY);
				}
			}
		}
		
		_RequestTapjoyConnect(appID, secretKey);
	}
	
	public static void transferDictionaryToObjectiveCWithName(Dictionary<string, System.Object> dictionary, string dictionaryName) {
		foreach (KeyValuePair<string, System.Object> kvp in dictionary) {
			// C# ToString() for bools is either "True" or "False" which is not good -- convert to "true" or "false"
			string dictValue;
			if (kvp.Value.GetType() == typeof(bool))
				dictValue = (Convert.ToBoolean(kvp.Value.ToString())) ? "true" : "false";
			else
				dictValue = kvp.Value.ToString();
			
			_SetKeyToValueInDictionary(kvp.Key, dictValue, dictionaryName);
		}
	}
	
	public static void EnableLogging(bool enable)
	{
		_EnableLogging(enable);
	}

	public static void SendSegmentationParams(Dictionary<string, System.Object> segmentationParams)
	{
		// transer data over to objective c
		transferDictionaryToObjectiveCWithName(segmentationParams, SEGMENTS_DICTIONARY_KEY);

		// tell objective c to call sendSegmentationParams with the dictionary we just transfered
		_SendSegmentationParams(SEGMENTS_DICTIONARY_KEY);
	}

	/// <summary>
	/// Tells Tapjoy that the application was paused / suspended
	/// </summary>
	public static void AppPause()
	{
		// Handled automatically on iOS
	}

	/// <summary>
	/// Tells Tapjoy that the application was resumed
	/// </summary>
	public static void AppResume()
	{
		// Handled automatically on iOS
	}
	
	public static void ActionComplete(string actionID)
	{
		_ActionComplete(actionID);
	}
	
	public static void SetUserID(string userID)
	{
		_SetUserID(userID);
	}
	
	public static void ShowOffers()
	{
		_ShowOffers();
	}
	
	public static void ShowOffersWithCurrencyID(string currencyID, bool isSelectorVisible)
	{
		_ShowOffersWithCurrencyID(currencyID, isSelectorVisible);
	}
	
	public static void GetFullScreenAd()
	{
		_GetFullScreenAd();
	}
	
	public static void GetFullScreenAdWithCurrencyID(string currencyID)
	{
		_GetFullScreenAdWithCurrencyID(currencyID);
	}
	
	public static void SetCurrencyMultiplier(float multiplier)
	{
		_SetCurrencyMultiplier(multiplier);
	}
		
	public static void ShowFullScreenAd()
	{
		_ShowFullScreenAd();
	}
		
	public static void ShowDefaultEarnedCurrencyAlert()
	{
		_ShowDefaultEarnedCurrencyAlert();
	}	
	
	public static void GetTapPoints()
	{
		_GetTapPoints();
	}
	
	public static void SpendTapPoints(int points)
	{
		_SpendTapPoints(points);
	}
	
	public static void AwardTapPoints(int points)
	{
		_AwardTapPoints(points);
	}
	
	public static int QueryTapPoints()
	{
		return _QueryTapPoints();
	}
	
	public static void GetDisplayAd()
	{
		_GetDisplayAd();
	}
	
	public static void ShowDisplayAd()
	{
		_ShowDisplayAd();
	}
	
	public static void HideDisplayAd()
	{
		_HideDisplayAd();
	}
	
	public static void GetDisplayAdWithCurrencyID(string currencyID)
	{
		_GetDisplayAdWithCurrencyID(currencyID);
	}
	
	public static void SetDisplayAdSize(string size)
	{
		_SetDisplayAdSize(size);
	}
	
	public static void EnableDisplayAdAutoRefresh(bool enable)
	{
		_EnableDisplayAdAutoRefresh(enable);	
	}
	
	public static void MoveDisplayAd(int x, int y)
	{
		_MoveDisplayAd(x, y);
	}
	
	public static void SetTransitionEffect(int transition)
	{
		_SetTransitionEffect(transition);
	}
	
	public static void SendShutDownEvent()
	{
		// This is handled automatically by iOS.
	}
	
	public static void SendIAPEvent(string name, float price, int quantity, string currencyCode)
	{
		_SendIAPEvent(name, price, quantity, currencyCode);
	}
	
	// Tapjoy Events --------
	
	public static void CreateEvent(string eventGuid, string eventName, string eventParameter)
	{
		_CreateEvent(eventGuid, eventName, eventParameter);
	}
	
	public static void SendEvent(string eventGuid)
	{
		_SendEvent(eventGuid);
	}
	
	public static void ShowEvent(string eventGuid)
	{
		_ShowEvent(eventGuid);
	}
	
	public static void EnableEventAutoPresent(string eventGuid, bool autoPresent)
	{
		_EnableEventAutoPresent(eventGuid, autoPresent);
	}

	public static void EnableEventPreload(string eventGuid, bool preload)
	{
		_EnableEventPreload(eventGuid, preload);
	}

	public static void EventRequestCompleted(string eventGuid)
	{
		_EventRequestCompleted(eventGuid);
	}
	
	public static void EventRequestCancelled(string eventGuid)
	{
		_EventRequestCancelled(eventGuid);
	}

	#endregion
}
#endif