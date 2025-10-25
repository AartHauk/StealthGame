using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float sprintingMultiplyer = 2f;
    public float gravityValue = -9.81f;
    private Vector2 currentMovementInput;

    public Transform pivotPoint;


    private CharacterController controller;
    public Animator animator;
    public override void OnStartClient()
    {
        if (IsOwner)
        {
            GetComponent<PlayerInput>().enabled = true;

            controller = GetComponent<CharacterController>();
            controller.enabled = true;
        }
    }

    public void OnMove(InputValue value)
    {
        currentMovementInput = value.Get<Vector2>();
    }

    // Ensure that crouching also affects physics collisions
    private bool crouched = false;
    private bool sprinting = false;
    public void OnCrouch(InputValue value)
    {
        if (value.isPressed)
        {
            crouched = !crouched;
        }
    }

    public void OnSprint(InputValue value)
    {
        if (value.Get<float>() > 0.001)
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }
    }

    private Vector3 lastLook = Vector3.forward;
    private Vector3 move = Vector3.zero;
    void Update()
    {
        if (!IsOwner) return;

        Vector3 moveDirection = new Vector3(currentMovementInput.x, 0f, currentMovementInput.y);
        moveDirection.Normalize();

        Vector3 lookDirection;

        if (moveDirection.magnitude > 0.0001)
        {
            moveDirection = GetComponent<PlayerSwitchCamera>().cameraRotation * moveDirection;
            moveDirection.Normalize();

            animator.SetFloat("Speed", 2);
            lookDirection = moveDirection;
            
        }
        else
        {
            moveDirection = GetComponent<PlayerSwitchCamera>().cameraRotation * moveDirection;
            moveDirection.Normalize();

            lookDirection = lastLook;
            animator.SetFloat("Speed", 0);

            sprinting = false;
        }

        if (controller.isGrounded && move.y < 0)
        {
            move.y = 0f;
        }

        if (crouched)
        {
            animator.SetBool("Crouched", true);
        }
        else
        {
            animator.SetBool("Crouched", false);
        }

        var tmp = move.y;
        move = moveSpeed * moveDirection;
        if (sprinting)
        {
            move *= sprintingMultiplyer;
            animator.SetFloat("Speed", 6);
        }
        move.y = tmp + gravityValue * Time.deltaTime;


        controller.Move(move * Time.deltaTime);
        pivotPoint.rotation = Quaternion.LookRotation(lookDirection);

        lastLook = lookDirection;
    }
}
