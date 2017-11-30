using System;

// todo: shutdown thread at the end
using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend : MonoBehaviour
{
	private static int localPort;

	// prefs
	private string IP;  // define in init
	public int port;  // define in init

	// "connection" things
	IPEndPoint remoteEndPoint;
	UdpClient client;

	string strMessage="";

	public void Start()
	{
		init();
	}

	// OnGUI
	void OnGUI()
	{
		Rect rectObj=new Rect(40,380,200,400);
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		GUI.Box(rectObj,"# UDPSend-Data\n127.0.0.1 "+port+" #\n"
			+ "shell> nc -lu 127.0.0.1  "+port+" \n"
			,style);

		// ------------------------
		// send it
		// ------------------------
		strMessage=GUI.TextField(new Rect(40,420,140,20),strMessage);
		if (GUI.Button(new Rect(190,420,40,20),"send"))
		{
			sendString(strMessage+"\n");
		}
	}

	public void init()
	{
		// define
		IP="127.0.0.1";
		port=5006;

		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
		client = new UdpClient();
	}

	private void sendString(string message)
	{
		try
		{
			if (message != "")
			{
				byte[] data = Encoding.UTF8.GetBytes(message);
				Debug.Log("sending " + IP + port);
				// Den message zum Remote-Client senden.
				client.Send(data, data.Length, remoteEndPoint);
			}
		}
		catch (Exception err)
		{
			print(err.ToString());
		}
	}
}
