using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCubeCreator : NetworkBehaviour
{
    public NetworkObject cubePrefab;

    public override void OnStartClient()
    {
        if (IsOwner)
        {
            GetComponent<PlayerInput>().enabled = true;
        }
    }

    public void OnFire(InputValue value)
    { 
        if (value.isPressed)
        {
            SpawnCube();
        }
    }

    [ServerRpc]
    private void SpawnCube()
    {
        NetworkObject obj = Instantiate(cubePrefab, transform.position, Quaternion.identity);
        obj.GetComponent<SyncMaterialColor>().color.Value = Random.ColorHSV();
        Spawn(obj);
    }

}
