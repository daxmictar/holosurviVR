using UnityEngine;
using Valve.VR;

public class VRMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 movementAction;
    public float speed = 1.0f; // Adjust this value as needed

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 movementInput = movementAction.GetAxis(SteamVR_Input_Sources.Any);

        // Move the player based on input
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        characterController.Move(transform.TransformDirection(movement) * speed * Time.deltaTime);
    }
}
