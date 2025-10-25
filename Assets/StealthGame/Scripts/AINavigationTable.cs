using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AINavigationTable : IEnumerable<AINavigationItem>
{
    [SerializeField] private List<AINavigationItem> navItems = new List<AINavigationItem>();

	[SerializeField] private UInt16[] timeTable = new UInt16[24 * 12]; // 5 min increments



	private AINavigationItem lastItem;
	public AINavigationItem Find (GameTime time)
	{
		if (lastItem == null) lastItem = navItems[0];

		var tableItem = timeTable[GetTimeTableIndex(time)];
		if (tableItem != 0) lastItem = navItems[tableItem - 1];
		
		return lastItem;
	}

	public void Add (AINavigationItem navItem)
	{
		navItems.Add(navItem);
		Sort();
	}

	public void Remove (AINavigationItem navItem)
	{
		navItems.Remove(navItem);
		Sort();
	}

	public void Sort ()
	{
		navItems.Sort();

		for (int i = 0; i < navItems.Count; i++)
		{
			var navItem = navItems[i];
			for (int t = GetTimeTableIndex(navItem.startTime); t <= GetTimeTableIndex(navItem.endTime); t++)
			{
				timeTable[t] = (ushort) (i + 1);
			}
		}
	}

	private int GetTimeTableIndex(GameTime time)
	{
		return (time.Hours * 12) + (int) Mathf.Round(time.Minutes * 12f / 60);
	}

	public IEnumerator<AINavigationItem> GetEnumerator ()
	{
		return navItems.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator ()
	{
		return GetEnumerator();
	}
}
