﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.VR;

using SimpleJSON;

public class UDPReceive : MonoBehaviour
{

	public float resetRFIDIDTimeout;

	private Thread receiveThread;
	private UdpClient client;
	private bool running = true;

	// public string IP = "127.0.0.1"; default local
	public int port;
	// define > init

	// infos
	public string lastReceivedUDPPacket = "";
	public int lastReceivedRFIDID = 0;
	private JSONArray lastReceivedCursors;

	private float rfidTimer = 0;

	public void Start ()
	{
		init ();
	}

	float tmptimer = 0;

	public void Update ()
	{
		rfidTimer += Time.deltaTime;
		if (rfidTimer > resetRFIDIDTimeout) {
			rfidTimer = 0;
			lastReceivedRFIDID = -1;
		}

		/*tmptimer += Time.deltaTime;
		if (tmptimer > 4) {
			tmptimer = -9999;
			lastReceivedUDPPacket = "{ \"rfid\": 9998}";
			ParseData ();
		}*/
	}

	private void init ()
	{
		receiveThread = new Thread (
			new ThreadStart (ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();
	}

	private  void ReceiveData ()
	{
		client = new UdpClient (port);
		while (running) {

			try {
				IPEndPoint anyIP = new IPEndPoint (IPAddress.Any, 0);
				byte[] data = client.Receive (ref anyIP);

				if (data == null || data.Length == 0) {
					Debug.Log ("Invalid data received, exiting..");
					return;
				}
				string text = Encoding.UTF8.GetString (data);
				lastReceivedUDPPacket = text;
				//Debug.Log("Received: " + lastReceivedUDPPacket);
			} catch (Exception err) {
				Debug.LogException (err);
			}
			ParseData ();
		}
	}

	void ParseData ()
	{
		JSONNode data = JSON.Parse (lastReceivedUDPPacket);
		if (data != null) {
			//Debug.Log ("data received");
			if (data ["cursors"] != null) {
				lastReceivedCursors = data ["cursors"].AsArray;
			} else if (data ["rfid"] != null) {
				lastReceivedRFIDID = data ["rfid"].AsInt;
				EventManager.ExecuteNFCScanned (lastReceivedRFIDID);
			} else if (data ["stock"] != null) {
				Debug.Log ("stock nr: " + data ["stock"].AsInt);
				int column = data ["stock"].AsInt;
				int stock = data ["value"].AsInt;
				switch (column) {
				case 0:
					Stock.colaStock = stock;
					break;
				case 1: 
					Stock.fantaStock = stock;
					break;
				case 2:
					Stock.minutemaidStock = stock;
					break;
				default:
					break;
				}
			} else if (data ["order"] != null) {
				Debug.Log ("Order is finished");
				if (CurrentState.currentState == State.PREPARING) {
					CurrentState.currentState = State.COMPLETED;
				}
			} else {
				Debug.LogError ("Invalid data received: " + data);
			}
		}
	}

	void OnDisable ()
	{ 
		//running = false;
		if (receiveThread != null)
			receiveThread.Abort (); 

		client.Close (); 
	}

	public JSONArray getLastCursors ()
	{
		return lastReceivedCursors;
	}

	public int getLastReceivedRFID ()
	{
		return lastReceivedRFIDID;
	}

	public string getLatestUDPPacket ()
	{
		return lastReceivedUDPPacket;
	}
}