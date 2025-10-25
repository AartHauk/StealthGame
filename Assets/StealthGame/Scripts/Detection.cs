using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detection : MonoBehaviour
{

    public List<VisionLayer> vLayers;
    public GameObject detectionAlert;
    public Image noticeBar;

    public float alertThresh;
    public float dectedThresh;

    public float timeToAlert = 2f;
    public float alertDecay = 1f;
    private float alertTimer = 0f;
    private bool alerted = false;

    private bool detected = false;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var layer in vLayers)
        {
            if (!layer) continue;
            layer.RegisterCallback(LayerDectected);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alerted)
        {
            alertTimer += Time.deltaTime;
        }
        else
        {
            alertTimer -= Time.deltaTime * alertDecay;
        }

        if (alertTimer > timeToAlert)
        {
            //detected = true;
            //detectionAlert.SetActive(true);
            alertTimer = timeToAlert;
        }
        else if (alertTimer <= 0)
        {
            alertTimer = 0;
        }

        noticeBar.fillAmount = alertTimer / timeToAlert;
        
    }

    // TODO Make detection take some time
    // TODO Make detection take into account the players state
    // TODO Decay alert overtime
    void LayerDectected(Collider other, float triggerValue, VisionLayer.Layer layer)
    {
        //PlayerDetected.PlayerState playerProps = other.GetComponent<PlayerDetected>().state;

        //alertDelta = (playerProps.speed + playerProps.height) * playerProps.light * triggerValue;

        float thresh = triggerValue;

        if (thresh > 0.001)
        {
            alerted = true;
        }
        else
        {
            alerted = false;
        }
    }

    public enum alertLevel
    {
        none,
        curious,
        cautious,
        alerted
    }
}
