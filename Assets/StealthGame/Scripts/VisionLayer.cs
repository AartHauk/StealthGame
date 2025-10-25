using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent (typeof(MeshCollider))]
public class VisionLayer : MonoBehaviour
{
    public Layer layer;

    public AnimationCurve falloff = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Space()]

    [Header("Shape Settings")]

    [Tooltip("Side-to-side dimension")]
    public int azimuth = 45;

    [Tooltip("Up-and-down dimension")]
    public int altitude = 45;

    [Tooltip("Distance for max detection distance")]
    public float radius = 1f;

    [Tooltip("Resolution of underlying mesh")]
    public int resolution = 4;

    public enum Layer
    {
        color,
        thermal,
        EM,
        audio
    }

    [HideInInspector]
    [SerializeField] private Mesh visionCone;
    private Action<Collider, float, Layer> triggerCallback;
    void Start()
    {
        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            gameObject.AddComponent<MeshCollider>();
            gameObject.GetComponent<MeshCollider>().convex = true;
            gameObject.GetComponent<MeshCollider>().isTrigger = true;
        }

        MakeMesh();
    }

    void MakeMesh()
    {
        visionCone = SphericalSector.GenerateMesh(azimuth, altitude, radius, resolution);
        gameObject.GetComponent<MeshCollider>().sharedMesh = visionCone;
    }

    public void RegisterCallback(Action<Collider, float, Layer> callback)
    {
        triggerCallback = callback;
    }

    #if UNITY_EDITOR
    private int lastAzi;
    private int lastAlt;
    private float lastRad;
    private int lastRes;
    #endif

    void Update()
    {
        #if UNITY_EDITOR
        if (lastAzi != azimuth || lastAlt != altitude || lastRad != radius || lastRes != resolution)
        {
            MakeMesh();

            lastAzi = azimuth;
            lastAlt = altitude;
            lastRad = radius;
            lastRes = resolution;
        }
        #endif
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LayerMask layerMask = LayerMask.GetMask("Player", "Enviroment");
            var t = (gameObject.transform.position - other.transform.position).magnitude / radius;

            Vector3 pos = transform.position;
            Vector3[] detectionPoints = other.gameObject.GetComponentsInParent<PlayerDetected>()[0].getDetectionPoints(layer);

            foreach (Vector3 point in detectionPoints)
            {
                Vector3 dir = point - pos;
                dir.Normalize();

                RaycastHit hitInfo;
                Physics.Raycast(pos, dir, out hitInfo, radius, layerMask);

                Collider check = hitInfo.collider;
                if (check is not null && check.CompareTag("Detection"))
                {
                    triggerCallback(other, falloff.Evaluate(t), layer);
                    break;
                }
            }
        }
    }

	private void OnTriggerExit (Collider other)
	{
	    if (other.CompareTag("Player"))
        {
            triggerCallback(other, 0, layer);
        }
	}

	void OnDrawGizmos()
    {
        switch (layer)
        {
            case Layer.color:
                Gizmos.color = new Color(0, 0, 255, 0.25f);
                break;
            case Layer.audio:
                Gizmos.color = new Color(0, 255, 0, 0.25f);
                break;
            default:
                Gizmos.color = new Color(255, 0, 0, 0.25f);
                break;
        }
        
        Gizmos.DrawMesh(visionCone, transform.position, transform.rotation);
    }

    void DefaultCallback(Collider other)
    {
        Debug.Log(other.gameObject.name + " triggerd " + gameObject.name);
    }
}
