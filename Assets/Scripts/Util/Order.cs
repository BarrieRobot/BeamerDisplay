using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class Order
{
	public string orderTime;
	public string item;

	public Order (string time, string item)
	{
		this.orderTime = time;
		this.item = item;
	}
}
