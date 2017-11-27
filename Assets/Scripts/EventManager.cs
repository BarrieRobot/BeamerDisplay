using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
	public delegate void CollideAction(int direction);
	public static event CollideAction OnClicked;


	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
		{
			if (OnClicked != null) {
				OnClicked (0);
			}
		}
	}

	public static void Execute () {
		if(OnClicked != null)
			OnClicked(0);
	}
}