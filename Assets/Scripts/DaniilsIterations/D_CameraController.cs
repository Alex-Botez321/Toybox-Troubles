using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class D_CameraController : MonoBehaviour
{
    private Vector2 mouseInput;
    private float xRot = 0;
    private float yRot = 0;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float sensitivity = 20;

    private const float sensMultiplier = 50f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;

    }

    public void onLook(InputAction.CallbackContext context)
    {
        if (player.GetComponent<PlayerController>().isInteracting)
            return;


        mouseInput = context.ReadValue<Vector2>();

        mouseInput.Normalize();

        yRot += mouseInput.x * sensitivity / sensMultiplier;
        xRot -= mouseInput.y * sensitivity / sensMultiplier;


        if (xRot > 60)
        {
            xRot = 60;
        }
        else if (xRot < -40)
        {
            xRot = -40;
        }

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        //if (Input.GetKey(KeyCode.Mouse1))
        //{

        //}
    }
}
