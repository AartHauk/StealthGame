using FishNet.Example.ColliderRollbacks;
using FishNet.Object;
using UnityEngine;

public class ServerDed : NetworkBehaviour {

    public Camera mainCamera;
    public override void OnStopClient()
    {
        mainCamera.enabled = true;
    }
}
