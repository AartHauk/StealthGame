using FishNet.Example.ColliderRollbacks;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitchCamera : NetworkBehaviour {

    public Camera playerCamera;
    public GameObject cameraPivot;

    public Quaternion cameraRotation;

    public float sensitivity = 1f;

    public override void OnStartClient () {
        if (IsOwner) {
            // GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
            playerCamera.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            cameraRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnLook (InputValue value) {
        if (playerCamera.enabled) {
            var dir = value.Get<Vector2>().normalized;

            cameraPivot.transform.RotateAround(cameraPivot.transform.position, Vector3.up, dir.x * sensitivity);
            playerCamera.transform.RotateAround(playerCamera.transform.position, playerCamera.transform.right, -dir.y * sensitivity);

            var rot = playerCamera.transform.rotation;
            if (45 < rot.eulerAngles.x && rot.eulerAngles.x <= 180) {
                playerCamera.transform.rotation = Quaternion.Euler(45f, rot.eulerAngles.y, rot.eulerAngles.z);
            } else if (180 < rot.eulerAngles.x && rot.eulerAngles.x < 360 - 45) {
                playerCamera.transform.rotation = Quaternion.Euler(-45f, rot.eulerAngles.y, rot.eulerAngles.z);
            }

            cameraRotation = cameraPivot.transform.rotation;
        }
    }

}
