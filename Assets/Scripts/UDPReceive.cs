using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.VR;

using SimpleJSON;

public class UDPReceive : MonoBehaviour {

	public float resetRFIDIDTimeout;

	private Thread receiveThread;
	private UdpClient client;
	private bool running = true;

	// public string IP = "127.0.0.1"; default local
	public int port; // define > init

	// infos
	public string lastReceivedUDPPacket="";
	public int lastReceivedRFIDID = 0;
	private JSONArray lastReceivedCursors;

	private float rfidTimer = 0;
	// start from shell
	private static void Main() {
		UDPReceive receiveObj = new UDPReceive();
		receiveObj.init();

		string text="";
		do {
			text = Console.ReadLine();
		}
		while(!text.Equals("exit"));
	}

	// start from unity3d
	public void Start() {
		init();
	}

	float tmptimer = 0;
	public void Update() {
		rfidTimer += Time.deltaTime;
		if (rfidTimer > resetRFIDIDTimeout) {
			rfidTimer = 0;
			lastReceivedRFIDID = -1;
		}

		tmptimer += Time.deltaTime;
		if (tmptimer > 4) {
			tmptimer = -9999;
			lastReceivedUDPPacket = "{ \"rfid\": 9999}";
			ParseData();
		}
	}

	// init
	private void init() {
		// status
		//print(" \t to 127.0.0.1 : "+port);
		//print("Test-Sending to this Port: nc -u 127.0.0.1  "+port+"");

		receiveThread = new Thread(
			new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();

	}

	// receive thread
	private  void ReceiveData() {

		client = new UdpClient(port);
		while (running) {

			try {
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);

				if (data == null || data.Length == 0){
					Debug.Log("Invalid data received, exiting..");
					return;
				}
				string text = Encoding.UTF8.GetString(data);
				lastReceivedUDPPacket=text;
				//Debug.Log("Received: " + lastReceivedUDPPacket);
			}
			catch (Exception err) {
				Debug.LogException (err);
			}
			ParseData ();
			//Thread.Sleep (100);
		}
	}

	public void SendChoice(string name) {
		Debug.Log ("sending");

		byte[] bytes = System.Convert.FromBase64String (name);
		client.Send(bytes, bytes.Length);
		Debug.Log ("sent");
	}

	void ParseData() {
		JSONNode data = JSON.Parse(lastReceivedUDPPacket);
		if (data != null) {
			//Debug.Log ("data received");
			if (data ["cursors"] != null) {
				lastReceivedCursors = data ["cursors"].AsArray;
			} else if (data ["rfid"] != null) {
				lastReceivedRFIDID = data ["rfid"].AsInt;
				EventManager.ExecuteNFCScanned (lastReceivedRFIDID);
			} else {
				Debug.LogError ("Invalid data received: " + data);
			}
		}
	}

	void OnDisable() 
	{ 
		//running = false;
		if ( receiveThread!= null) 
			receiveThread.Abort(); 

		client.Close(); 
	} 

	public JSONArray getLastCursors() {
		return lastReceivedCursors;
	}

	public int getLastReceivedRFID() {
		return lastReceivedRFIDID;
	}

	// getLatestUDPPacket
	// cleans up the rest
	public string getLatestUDPPacket() {
		return lastReceivedUDPPacket;
	}
}