using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class Order
{
	public string orderTime;
	public string item;
	public int itemid;

	public Order (string time, string item, int itemid)
	{
		this.orderTime = time;
		this.item = item;
		this.itemid = itemid;
	}
}
