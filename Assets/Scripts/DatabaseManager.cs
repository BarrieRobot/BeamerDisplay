using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEditor;
using SimpleFirebaseUnity;

//using NUnit.Framework;
using System.Configuration;
using SimpleJSON;
using SimpleFirebaseUnity.MiniJSON;

public class DatabaseManager : MonoBehaviour
{
	Firebase firebase;
	Firebase orders;
	Firebase barrie;
	public static List<Order> lastReceivedOrders;
	public static Dictionary<int, float> prices;
	public static bool ordersFound = false;

	void Start ()
	{
		firebase = Firebase.CreateNew ("iona-4d244.firebaseio.com", "dUS0OWpxo7cGor2C81N2ffPbvliYQIc69jLDTO5m");
		// Init callbacks
		/*firebase.OnGetSuccess += GetOKHandler;
		firebase.OnGetFailed += GetFailHandler;
		firebase.OnSetSuccess += SetOKHandler;
		firebase.OnSetFailed += SetFailHandler;
		firebase.OnUpdateSuccess += UpdateOKHandler;
		firebase.OnUpdateFailed += UpdateFailHandler;
		firebase.OnPushSuccess += PushOKHandler;
		firebase.OnPushFailed += PushFailHandler;
		firebase.OnDeleteSuccess += DelOKHandler;
		firebase.OnDeleteFailed += DelFailHandler;
*/
		barrie = firebase.Child ("Barrie", true);
		orders = firebase.Child ("shared", true).Child ("orders", true);

		lastReceivedOrders = new List<Order> ();

		// Make observer on "last update" time stamp
		FirebaseObserver observer = new FirebaseObserver (barrie, 1f);
		observer.OnChange += (Firebase sender, DataSnapshot snapshot) => {
			DoDebug ("[OBSERVER] Last updated changed to: " + snapshot.Value<long> ());
		};
		observer.Start ();
		//getExistingOrders (9999);
		getPrices ();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void insertOrder (int nfcID, Drink drink, string name = "")
	{
		Firebase myOrders = orders.Child ("" + nfcID);
		myOrders.Push ("{ \"drink\": \"" + (int)drink + "\", \"name\": \"" + name + "\", \"time\": \"" + System.DateTime.Now.ToString ("dd/MM/yyyy HH:mm:ss") + "\", \"completed\": \"" + "true" + "\" }", true); //true makes it create child objects
	}

	public List<Order> getExistingOrders (int nfcID)
	{
		ordersFound = false;
		Firebase myOrders = orders.Child ("" + nfcID);
		myOrders.OnGetSuccess += GetOrdersHandler;
		myOrders.GetValue ();
		return null;
	}

	public void getPrices ()
	{
		Firebase options = barrie.Child ("options");
		options.OnGetSuccess += GetPricesHandler;
		options.GetValue ();
	}

	static void GetPricesHandler (Firebase sender, DataSnapshot snapshot)
	{
		prices = new Dictionary<int, float> ();
		JSONArray items = JSON.Parse (snapshot.RawJson).AsArray;
		Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>> ();
		int i = 0;
		foreach (JSONObject item in items) {
			prices.Add (i, item ["price"].AsFloat);
			i++;
		}
	}

	static void GetOrdersHandler (Firebase sender, DataSnapshot snapshot)
	{
		DoDebug ("[OK] Get from key: <" + sender.FullKey + ">");
		DoDebug ("[OK] Raw Json: " + snapshot.RawJson);
		Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>> ();
		List<string> keys = snapshot.Keys;
		lastReceivedOrders.Clear ();
		JSONObject orders = JSON.Parse (snapshot.RawJson).AsObject;
		Debug.Log (orders.Count);
		bool incompleteFound = false;
		for (int i = 0; i < orders.Count; i++) {
			JSONObject order = orders [i].AsObject;
			if (!order ["completed"].AsBool) {
				incompleteFound = true;
				Debug.LogError (order);
				lastReceivedOrders.Add (new Order (order ["time"], order ["name"]));
			}
		}
		if (incompleteFound)
			ordersFound = true;
		else
			ordersFound = false;
	}

	static void GetTimeStamp (Firebase sender, DataSnapshot snapshot)
	{
		long timeStamp = snapshot.Value<long> ();
		DateTime dateTime = Firebase.TimeStampToDateTime (timeStamp);

		DoDebug ("[OK] Get on timestamp key: <" + sender.FullKey + ">");
		DoDebug ("Date: " + timeStamp + " --> " + dateTime.ToString ());
	}

	static void DoDebug (string str)
	{
		Debug.Log (str);
	}
}
