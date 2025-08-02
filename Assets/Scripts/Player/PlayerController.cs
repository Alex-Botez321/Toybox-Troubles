using Cinemachine;
using Cinemachine.Utility;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Player Input
    [SerializeField] private InputAction playerController;
    [SerializeField] private Controls playerControls;
    #endregion

    #region Components
    private Rigidbody rb;
    [SerializeField] private Animator animator;
    #endregion

    #region Movement and Camera variables
    //Movement Variables
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] public float speed = 5f;
    //[SerializeField] private float acceleration = 1f;
    //[SerializeField] private float decceleration = 1f;
    public Vector2 moveDirection = Vector2.zero;
    private Vector2 currentSpeed = Vector2.zero;


    //Camera Variables
    [SerializeField] private Transform cameraTarget = null;
    [SerializeField] private Transform model = null;
    Vector3 camForward;
    Vector3 camRight;
    [SerializeField] private Camera actualCamera;
    #endregion

    #region Jump Variables
    [HideInInspector] public bool canJump = false;
    [HideInInspector] public Vector3[] jumpCoords;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public int jumpPoint = 20;
    [HideInInspector] public float maxTime = 0.05f; //Time to complete (curveSmoothness / 100)% of the jump
    private Coroutine jumpLerp;
    public GameObject currentJump;
    #endregion

    #region Other Variables
    [HideInInspector] public bool isInteracting = false;

    //Walking Audio and Raycast Variables
    [SerializeField] private GameObject WalkSource;
    public AudioSource audioSource;
    public RaycastHit walkSurfaceHit;
    [SerializeField] Transform rayStart;
    [SerializeField] float rayRange = 1;
    [SerializeField] private LayerMask layerMask;
    
    #endregion

    [SerializeField] private Transform temp;

    private void Awake()
    {
        playerControls = new Controls();
        jumpCoords = new Vector3[BezierJump.curveSmoothness];
        rb = GetComponent<Rigidbody>();
        //animator = GetComponentInChildren<Animator>();

        

        if (cameraTarget == null)
            Debug.Log("<color=red>Error: </color>Camera target reference missing in Player -> PlayerController");
        if (model == null)
            Debug.Log("<color=red>Error: </color>Model reference missing in Player -> PlayerController");

        StartCoroutine(RandomIdle());
    }

    private void Start()
    {
        WalkSource.SetActive(false);
        //InteractOff();
        
    }

    void Update()
    {
        camForward = temp.transform.forward;
        camRight = temp.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        animator.SetInteger("JumpPoint", jumpPoint);
    }

    private void FixedUpdate()
    {
        if (isInteracting)
            return;

        //Calculate the direction the player is moving relative to the camera
        Vector3 forwardRelative = moveDirection.y * camForward;
        Vector3 rightRelative = moveDirection.x * camRight;
        Vector3 moveDir = forwardRelative + rightRelative;
        

        //var totalSpeed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
        //Debug.Log(totalSpeed);
        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z));

        if (moveDir != Vector3.zero)
        {
            //Setting the velocity of the player
            rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
            //if(rb.velocity.magnitude < maxSpeed)
            //{
            //rb.velocity = new Vector3(moveDir.x * Mathf.Lerp(0, maxSpeed, currentSpeed.x), 
            //                            rb.velocity.y, 
            //                            moveDir.z * Mathf.Lerp(0, maxSpeed, currentSpeed.y));

            //if (currentSpeed.x <= 1)
            //{
            //    currentSpeed.x += acceleration * Time.deltaTime;
            //    currentSpeed.y += acceleration * Time.deltaTime;
            //}
            //}


            //moving the player model
            float targetRotation = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float rotationAngle = Mathf.MoveTowardsAngle(this.gameObject.transform.GetChild(1).transform.eulerAngles.y, targetRotation, rotationSpeed);
            this.gameObject.transform.GetChild(1).transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            
        }
        else
        {
            
            //rb.velocity = new Vector3(Mathf.Lerp(0, maxSpeed, currentSpeed.x),
            //                                rb.velocity.y,
            //                                Mathf.Lerp(0, maxSpeed, currentSpeed.y));
            //if(currentSpeed.x >= 0)
            //{
            //    currentSpeed.x -= decceleration * Time.deltaTime;
            //    currentSpeed.y -= decceleration * Time.deltaTime;
            //}
        }

        if(rb.velocity.magnitude > 0)
        {

            //WalkSource.SetActive(true);
            if (!audioSource.isPlaying)
            {
                OnWalkSound();
                Debug.DrawRay(transform.position, transform.up * rayRange * -1, Color.cyan);
            }
        }
        else 
        {
            //WalkSource.SetActive(false);
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Lerps the player from 1 Vector3 to the next in jumpCoords
    /// </summary>
    IEnumerator JumpLerp()
    {
        float timeElapsed = 0;

        #region Attempt at rotating player to face jump direction
        //Vector3 startPos = transform.position;
        //get player forward
        //get target vector at height of player
        //get direction to target (targetPos - startPos) then normalize

        //Vector3 playerForward = transform.forward;
        //Vector3 targetVector = new Vector3(jumpCoords[19].x, transform.position.y, jumpCoords[19].z);

        //float dotProduct = Vector3.Dot(playerForward, targetVector);


        //var targetRot = dotProduct / (startPos.magnitude * targetVector.magnitude);

        //Debug.Log(targetRot);
        //transform.rotation = Quaternion.Euler(0f, targetRot, 0f);
        #endregion

        while (timeElapsed < maxTime)
        {
            transform.position = Vector3.Lerp(jumpCoords[jumpPoint], jumpCoords[jumpPoint + 1], timeElapsed / maxTime);
            


            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //If the jump is not over restart routine at the next point
        if (jumpPoint < jumpCoords.Length - 2)//idk why it has to be minus 2 but it works
        {
            jumpPoint++;
            transform.position = jumpCoords[jumpPoint];//smoothens last point jump segment
            StartCoroutine(JumpLerp());
        }
        else //If the jump is over reset the player to normal
        {
            transform.position = jumpCoords[jumpPoint + 1];//smoothens last point jump last segment
            jumpPoint = 0;
            isJumping = false;
            jumpLerp = null;
            rb.velocity = Vector3.zero;//resetting velocity to ensure there is no fuckery
            animator.SetInteger("JumpPoint", jumpPoint);
            StopCoroutine(JumpLerp());
        }
    }

    /// <summary>
    /// If the player pressed jump and is in a jumpable area start jump Coroutine
    /// </summary>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(canJump)
            {
                if (jumpLerp == null)
                {
                    if(jumpPoint<1)
                    {
                        
                        jumpLerp = StartCoroutine(JumpLerp());
                    }
                }
            }
        }
    }

    /// <summary>
    /// Get The keyboard inputs for movement
    /// </summary>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void InteractOn()
    {
        Cursor.visible = true;
        isInteracting = true;
    }

    public void InteractOff()
    {
        Cursor.visible = false;
        isInteracting = false;
    }

    private void OnWalkSound()
    {
        layerMask = ~layerMask;
        //audioSource.PlayOneShot(audioSource.clip);
        if (Physics.Raycast(transform.position, transform.up * -1, out walkSurfaceHit, rayRange, layerMask))
        {
            WalkingAudio audioClip; 
            if(walkSurfaceHit.transform.gameObject.TryGetComponent<WalkingAudio>(out audioClip))
            {
                audioSource.PlayOneShot(audioClip.AudioClipGet());
            }
        }
    }

    IEnumerator RandomIdle()
    {
        var idle = UnityEngine.Random.Range((int)0, (int)3);
        animator.SetInteger("Idle", idle);

        yield return new WaitForSeconds(5);

        StartCoroutine(RandomIdle());
    }

}