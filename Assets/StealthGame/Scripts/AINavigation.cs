using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
	public WorldClock clock;
	public AINavigationTable navTable;

    private NavMeshAgent agent;

    void Start()
    {
        if (clock == null)
        {
            clock = GameObject.Find("ClockController").GetComponent<WorldClock>();
        }
		
        agent = GetComponent<NavMeshAgent>();

        navTable.Sort();
    }

    void Update()
    {
        agent.destination = navTable.Find(clock.GetTime()).transform.position;
	}
}
