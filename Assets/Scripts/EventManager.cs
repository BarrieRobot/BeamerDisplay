using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
	public delegate void CollideAction(Direction direction);
	public static event CollideAction OnClicked;

	public delegate void CollideActionSelect();
	public static event CollideActionSelect OnClickedSelect;

	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 150, 5, 100, 30), "Click"))
		{
			if (OnClicked != null) {
				OnClicked (Direction.LEFT);
			}
		}
		if(GUI.Button(new Rect(Screen.width / 2 + 50, 5, 100, 30), "Click"))
		{
			if (OnClicked != null) {
				OnClicked (Direction.RIGHT);
			}
		}
	}

	public static void Execute (Direction dir) {
		if(OnClicked != null)
			OnClicked(dir);
	}

	public static void ExecuteSelect () {
		if(OnClickedSelect != null)
			OnClickedSelect();
	}
}