﻿using System;
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
	private bool internetworking = true;
	static List<int> coldlist = new List<int> (new int[] { 4, 5, 6 });
	static List<int> hotlist = new List<int> (new int[]{ 0, 1, 2, 3 });

	void Start ()
	{
		StartCoroutine (checkInternetConnection ((isConnected) => {
			if (!isConnected) {
				//Debug.LogError ("Error. Check internet connection!");
				internetworking = false;
			} else {
				internetworking = true;
				firebase = Firebase.CreateNew ("iona-4d244.firebaseio.com", "dUS0OWpxo7cGor2C81N2ffPbvliYQIc69jLDTO5m");
				barrie = firebase.Child ("Barrie", true);
				orders = firebase.Child ("shared", true).Child ("orders", true);
				lastReceivedOrders = new List<Order> ();
				getPrices ();
			}
		}));
	}

	IEnumerator checkInternetConnection (Action<bool> action)
	{
		WWW www = new WWW ("http://google.com");
		yield return www;
		if (www.error != null) {
			action (false);
		} else {
			action (true);
		}
	}

	void OnGUI ()
	{
		if (!internetworking) {
			GUI.skin.textField.fontSize = 20;
			GUIUtility.ScaleAroundPivot (new Vector2 (-1, 1), new Vector2 (Screen.width / 2, Screen.height / 2)); 
			GUI.TextField (new Rect (Screen.width / 2 - 125, Screen.height / 2 - 150, 250, 30), "Geen internet verbinding");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void insertOrder (int nfcID, int drinkid, string name = "")
	{
		if (internetworking) {
			Firebase myOrders = orders.Child ("" + nfcID);
			myOrders.Push ("{ \"drink\": \"" + drinkid + "\", \"name\": \"" + name + "\", \"time\": \"" + System.DateTime.Now.ToString ("dd/MM/yyyy HH:mm:ss") + "\", \"completed\": \"" + "true" + "\" }", true); //true makes it create child objects
		} else {
			StartCoroutine (checkInternetConnection ((isConnected) => {
				if (!isConnected) {
					internetworking = false;
				} else {
					internetworking = true;
					firebase = Firebase.CreateNew ("iona-4d244.firebaseio.com", "dUS0OWpxo7cGor2C81N2ffPbvliYQIc69jLDTO5m");
					barrie = firebase.Child ("Barrie", true);
					orders = firebase.Child ("shared", true).Child ("orders", true);
					lastReceivedOrders = new List<Order> ();
					getPrices ();
				}
			}));
		}
	}

	public void getExistingOrders (int nfcID)
	{
		if (internetworking) {
			if (orders != null) {
				ordersFound = false;
				Firebase myOrders = orders.Child ("" + nfcID);
				myOrders.OnGetSuccess += GetOrdersHandler;
				myOrders.GetValue ();
			} 
		} else {
			StartCoroutine (checkInternetConnection ((isConnected) => {
				if (!isConnected) {
					internetworking = false;
				} else {
					internetworking = true;
					firebase = Firebase.CreateNew ("iona-4d244.firebaseio.com", "dUS0OWpxo7cGor2C81N2ffPbvliYQIc69jLDTO5m");
					barrie = firebase.Child ("Barrie", true);
					orders = firebase.Child ("shared", true).Child ("orders", true);
					lastReceivedOrders = new List<Order> ();
					getPrices ();
				}
			}));
		}
	}

	public void getPrices ()
	{
		if (internetworking) {
			Firebase options = barrie.Child ("options");
			options.OnGetSuccess += GetPricesHandler;
			options.GetValue ();
		} else {
			StartCoroutine (checkInternetConnection ((isConnected) => {
				if (!isConnected) {
					internetworking = false;
				} else {
					internetworking = true;
					firebase = Firebase.CreateNew ("iona-4d244.firebaseio.com", "dUS0OWpxo7cGor2C81N2ffPbvliYQIc69jLDTO5m");
					barrie = firebase.Child ("Barrie", true);
					orders = firebase.Child ("shared", true).Child ("orders", true);
					lastReceivedOrders = new List<Order> ();
					getPrices ();
				}
			}));
		}
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
		bool incompleteFound = false;
		lastReceivedOrders.Clear ();
		if (JSON.Parse (snapshot.RawJson) != null) {
			List<string> keys = snapshot.Keys;
			JSONObject orders = JSON.Parse (snapshot.RawJson).AsObject;
			for (int i = 0; i < orders.Count; i++) {
				JSONObject order = orders [i].AsObject;
				if (!order ["completed"].AsBool) {
					incompleteFound = true;
					DrinkType dtype = coldlist.IndexOf (order ["drink"].AsInt) != -1 ? DrinkType.COLD : DrinkType.HOT;
					lastReceivedOrders.Add (new Order (order ["time"], order ["name"], order ["drink"].AsInt, dtype));
				}
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
