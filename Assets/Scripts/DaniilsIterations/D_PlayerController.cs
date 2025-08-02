using Cinemachine;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class D_PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private InputAction playerController;
    Vector2 MoveDirection;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float moveSpeed = 5f;
    private bool JumpInput = false;
    private float jumpInputStartTime;
    public Vector2 moveDirection = Vector2.zero;
    [SerializeField]
    private Controls playerControls;
    [SerializeField]
    private float rotationSpeed;
    private float rotationAngle;

    [SerializeField]
    private Transform cam;
    [SerializeField]
    public CinemachineVirtualCamera virtualCamera;

    Vector3 camForward;
    Vector3 camRight;


    public Vector3 targetPos;
    public float jumpSpeed = 0.5f;
    public float arcHeight = 1;
    private Vector3 startPos;
    private GetJumpData jumpData;
    private Vector3 nextPos;


    public bool canJump = false;
    [SerializeField]
    public Vector3[] jumpCoords = new Vector3[20];
    private bool jumping = false;
    private int jumpPoint = 0;
    private float jumpFraction = 0;

    public bool isInteracting = false;

    private void Awake()
    {
        playerControls = new Controls();
    }


    // Update is called once per frame
    void Update()
    {
        camForward = cam.forward;
        camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;

        if (jumping)
        {
            // NOTE: this is changing framerate when we jump to make it less choppy.
            // this is a temporary solution becuase it will slow down the game around it, 
            // Maaaaaaybe we'll chang it later, if we can
            Time.fixedDeltaTime = 0.3f;
            //rb.mass = 0f;
            if (jumpPoint < 19)
            {
                jumpFraction += jumpSpeed * Time.deltaTime;
                jumpLogic();

            }
            else
            {
                jumpPoint = 0;
                jumping = false;
                //rb.mass = 1f;
                Time.fixedDeltaTime = 0.02f;
            }
        }
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    isInteracting = true;
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    isInteracting = false;
        //}
    }

    private void FixedUpdate()
    {
        if (isInteracting)
            return;

        Vector3 forwardRelative = moveDirection.y * camForward;
        Vector3 rightRelative = moveDirection.x * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

        if (moveDir != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float rotationAngle = Mathf.MoveTowardsAngle(this.gameObject.transform.GetChild(1).transform.eulerAngles.y, targetRotation, rotationSpeed);
            this.gameObject.transform.GetChild(1).transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

            //Quaternion inputRotation = Quaternion.LookRotation(moveDir);
            //rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, inputRotation,Time.deltaTime));

        }
    }

    private void jumpLogic()
    {
        if (jumpFraction < 1)
        {
            transform.position = Vector3.Lerp(jumpCoords[jumpPoint], jumpCoords[jumpPoint + 1], jumpFraction);
        }
        else
        {
            jumpPoint++;
            jumpFraction = 0f;
        }

    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canJump)
            {
                jumping = true;
            }
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private bool IsGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Jumps")
        //{
        //    canJump = true;

        //    jumpCoords = other.GetComponent<BezierJump>().getPoints();
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Jumps")
        {
            canJump = false;
        }
    }

    public void jumpTrigger()
    {
        canJump = true;
    }

    public void changeCameraLookAt(Transform newTarget)
    {
        virtualCamera.LookAt = newTarget;
    }
}
