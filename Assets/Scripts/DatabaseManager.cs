using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SimpleFirebaseUnity;

public class DatabaseManager : MonoBehaviour {
	Firebase firebase;
	Firebase orders;

	void Start () {
		firebase = Firebase.CreateNew ("iona-4d244.firebaseio.com", "dUS0OWpxo7cGor2C81N2ffPbvliYQIc69jLDTO5m");
		// Init callbacks
		firebase.OnGetSuccess += GetOKHandler;
		firebase.OnGetFailed += GetFailHandler;
		firebase.OnSetSuccess += SetOKHandler;
		firebase.OnSetFailed += SetFailHandler;
		firebase.OnUpdateSuccess += UpdateOKHandler;
		firebase.OnUpdateFailed += UpdateFailHandler;
		firebase.OnPushSuccess += PushOKHandler;
		firebase.OnPushFailed += PushFailHandler;
		firebase.OnDeleteSuccess += DelOKHandler;
		firebase.OnDeleteFailed += DelFailHandler;

		Firebase barrie = firebase.Child("Barrie", true);
		orders = firebase.Child("shared", true).Child ("orders", true);

		// Make observer on "last update" time stamp
		FirebaseObserver observer = new FirebaseObserver(barrie, 1f);
		observer.OnChange += (Firebase sender, DataSnapshot snapshot)=>{
			DoDebug("[OBSERVER] Last updated changed to: " + snapshot.Value<long>());
		};
		observer.Start ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void insertOrder (int nfcID, Drink drink, string name = "")
	{
		Firebase myOrders = orders.Child ("" + nfcID);
		myOrders.Push ("{ \"drink\": \"" + (int)drink + "\", \"name\": \"" + name + "\", \"time\": \"" + System.DateTime.Now + "\" }", true); //true makes it create child objects

	}

	static void GetOKHandler(Firebase sender, DataSnapshot snapshot)
	{
		DoDebug("[OK] Get from key: <" + sender.FullKey + ">");
		DoDebug("[OK] Raw Json: " + snapshot.RawJson);

		Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
		List<string> keys = snapshot.Keys;

		if (keys != null)
			foreach (string key in keys)
			{
				DoDebug(key + " = " + dict[key].ToString());
			}
	}

	static void GetFailHandler(Firebase sender, FirebaseError err)
	{
		DoDebug("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
	}

	static void SetOKHandler(Firebase sender, DataSnapshot snapshot)
	{
		DoDebug("[OK] Set from key: <" + sender.FullKey + ">");
	}

	static void SetFailHandler(Firebase sender, FirebaseError err)
	{
		DoDebug("[ERR] Set from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
	}

	static void UpdateOKHandler(Firebase sender, DataSnapshot snapshot)
	{
		DoDebug("[OK] Update from key: <" + sender.FullKey + ">");
	}

	static void UpdateFailHandler(Firebase sender, FirebaseError err)
	{
		DoDebug("[ERR] Update from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
	}

	static void DelOKHandler(Firebase sender, DataSnapshot snapshot)
	{
		DoDebug("[OK] Del from key: <" + sender.FullKey + ">");
	}

	static void DelFailHandler(Firebase sender, FirebaseError err)
	{
		DoDebug("[ERR] Del from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
	}

	static void PushOKHandler(Firebase sender, DataSnapshot snapshot)
	{
		DoDebug("[OK] Push from key: <" + sender.FullKey + ">");
	}

	static void PushFailHandler(Firebase sender, FirebaseError err)
	{
		DoDebug("[ERR] Push from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
	}

	static void GetRulesOKHandler(Firebase sender, DataSnapshot snapshot)
	{
		DoDebug("[OK] GetRules");
		DoDebug("[OK] Raw Json: " + snapshot.RawJson);
	}
	static void GetRulesFailHandler(Firebase sender, FirebaseError err)
	{
		DoDebug("[ERR] GetRules,  " + err.Message + " (" + (int)err.Status + ")");
	}

	static void GetTimeStamp(Firebase sender, DataSnapshot snapshot)
	{
		long timeStamp = snapshot.Value<long> ();
		DateTime dateTime = Firebase.TimeStampToDateTime (timeStamp);

		DoDebug ("[OK] Get on timestamp key: <" + sender.FullKey + ">");
		DoDebug("Date: " + timeStamp + " --> " + dateTime.ToString ());
	}

	static void DoDebug(string str)
	{
		Debug.Log(str);
	}
}
