using FishNet.Object;
using System.Collections;
using UnityEngine;

public class DespawnAfterTime : NetworkBehaviour
{

    public float secondsBeforeDespwan = 3f;

    public override void OnStartServer()
    {
        StartCoroutine(DespawnAfterSeconds());
    }

    private IEnumerator DespawnAfterSeconds()
    {
        yield return new WaitForSeconds(secondsBeforeDespwan);

        Despawn();
    }
    
}
