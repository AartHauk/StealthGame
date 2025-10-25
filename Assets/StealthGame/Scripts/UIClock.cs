using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIClock : MonoBehaviour
{

    public WorldClock clock;
    private TMP_Text txt;
    void Start()
    {
        if (clock == null)
        {
            clock = GameObject.Find("ClockController").GetComponent<WorldClock>();
        }
        txt = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var time = clock.GetTime();
        txt.text = time.ToString();
    }
}
