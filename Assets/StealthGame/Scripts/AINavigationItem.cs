using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AINavigationItem : IComparable<AINavigationItem>
{
	[SerializeField] public GameTime startTime;
	[SerializeField] public GameTime endTime;
	[SerializeField] public Transform transform;
	[SerializeField] public AIAction action;

	public int CompareTo (AINavigationItem other)
	{
		return this.startTime.CompareTime(other.startTime);
	}

	public override int GetHashCode ()
	{
		return this.startTime.GetHashCode();
	}
}

public enum AIAction
{
	move,
	wait
}