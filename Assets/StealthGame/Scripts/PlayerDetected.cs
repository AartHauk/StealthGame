using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : MonoBehaviour
{
    public Transform[] detectionPoints;
    public Dictionary<Transform, VisionLayer.Layer> dPoints;

    public struct PlayerState
    {
        public float speed;
        public float height;
        public float light;
    }

    public PlayerState state;

    // void Start()
    // {
    //     dPoints = new Dictionary<Transform, VisionLayer.Layer>();
    //     foreach (Transform t in detectionPoints)
    //     {
    //         dPoints.Add(t, VisionLayer.Layer.color);
    //     }
    // }
    public Vector3[] getDetectionPoints(VisionLayer.Layer layer)
    {
        // List<Vector3> outPoints = new List<Vector3>();

        // foreach (var kvp in dPoints)
        // {
        //     if (kvp.Value == layer)
        //         outPoints.Add(kvp.Key.position);
        // }

        // return outPoints.ToArray();

        Vector3[] outPoints = new Vector3[detectionPoints.Length];
        for (int i = 0; i < detectionPoints.Length; i++) {
            outPoints[i] = detectionPoints[i].position;
        }
        return outPoints;
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // foreach (var kvp in dPoints)
        // {
        //     GetColor(kvp.Value);
        //     Gizmos.DrawSphere(kvp.Key.transform.position, 0.1f);
        // }

        foreach (var point in detectionPoints)
        {
            if (!point) continue;
            GetColor(VisionLayer.Layer.color);
            Gizmos.DrawSphere(point.position, 0.1f);
        }
    }

    void GetColor(VisionLayer.Layer layer)
    {
        switch (layer)
        {
            case VisionLayer.Layer.color:
                Gizmos.color = Color.blue;
                break;
            case VisionLayer.Layer.thermal:
                Gizmos.color = Color.red;
                break;
            case VisionLayer.Layer.EM:
                Gizmos.color = Color.white;
                break;
            case VisionLayer.Layer.audio:
                Gizmos.color = Color.green;
                break;
            default:
                Gizmos.color = Color.black;
                break;
        }
    }
    #endif
}
