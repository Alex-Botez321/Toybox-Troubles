using Cinemachine;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Schema;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private Vector2 mouseInput;
    private float xRot = 0;
    private float yRot = 0;

    [SerializeField] private GameObject player;
    [SerializeField] private float sensitivity = 20.0f;
    [SerializeField] private float heightOffSet = 0.25f;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float maxFoV = 65;
    [SerializeField] private float timeToRotate180 = 1.0f;
    [SerializeField] private float maximumPitch = 60f;
    [SerializeField] private float minimumPitch = -30f;
    [SerializeField] private float fovChangePitch = -15f;
    private float timeToRotate;
    private float defaultFoV = 60;
    private Rigidbody playerRB;
    private Quaternion localModelRotation;
    [SerializeField] private CinemachineBrain cameraBrain;
    private float sensMultiplier = 1;


    private void Awake()
    {
        if (player == null)
            Debug.Log("<color=red>Error: </color>Player reference missing in CameraTarget -> CameraController <color=blue> goodmorning</color>");
        if (playerCamera == null)
            Debug.Log("<color=red>Error: </color>Player Camera reference missing in CameraTarget -> CameraController");
        playerRB = player.GetComponent<Rigidbody>();
        timeToRotate = timeToRotate180 / 180;
        sensMultiplier = PlayerPrefs.GetFloat("sensitivity");
    }

    private void Update()
    {
        if (player.GetComponent<PlayerController>().isInteracting || cameraBrain.IsBlending)
            return;

        sensMultiplier = PlayerPrefs.GetFloat("sensitivity");

        yRot += mouseInput.x * sensitivity * sensMultiplier;
        xRot -= mouseInput.y * sensitivity * sensMultiplier;



        if (xRot > maximumPitch)
            xRot = maximumPitch;
        else if (xRot < fovChangePitch)
        {
            playerCamera.m_Lens.FieldOfView = math.abs(xRot + 20) / 2 + defaultFoV;
            if (playerCamera.m_Lens.FieldOfView > maxFoV)
                playerCamera.m_Lens.FieldOfView = maxFoV;
            if (xRot < minimumPitch)
                xRot = minimumPitch;
        }


        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        defaultFoV = playerCamera.m_Lens.FieldOfView;
        transform.position = new Vector3(player.transform.position.x,
                                        player.transform.position.y - heightOffSet,
                                        player.transform.position.z);
    }

    #region fancy camera jazz
    //private void FixedUpdate()
    //{
    //    //player velocity ranges from -5 to 5 for x and z axis
    //    //player velocity magnitude 0 to 5

    //    if (Input.GetKeyDown("space"))
    //    {
    //        StartCoroutine(cameraAdjustment(timeToRotate * calculateChangeInDegrees()));
    //        //transform.localRotation = localModelRotation;
    //    }

    //}

    //IEnumerator cameraAdjustment(float maxTime)
    //{
    //    var temp = transform.rotation;
    //    float timeElapsed = 0;
    //    localModelRotation = player.transform.GetChild(1).transform.localRotation;
    //    while (timeElapsed < maxTime)
    //    {
    //        transform.localRotation = Quaternion.Lerp(temp,
    //                                        localModelRotation,
    //                                        timeElapsed/maxTime);
    //        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0f, transform.rotation.w);
    //        timeElapsed += Time.deltaTime;

    //        //if (Mathf.Abs(transform.localRotation.y - localModelRotation.y) <= 0.005)
    //        if(transform.rotation == localModelRotation)
    //        {
    //            timeElapsed = 253;
    //            yield break;
    //        }

    //        //Debug.Log(transform.rotation.eulerAngles);

    //        xRot = transform.rotation.eulerAngles.x;
    //        yRot = transform.rotation.eulerAngles.y;
    //        yield return null;
    //    }

    //    yield break;
    //}
    /// <summary>
    /// Calculates how many degrees the camera needs to rotate across both X and Y axis
    /// </summary>
    /// <returns>Total degrees needed to rotate camera </returns>
    //private float calculateChangeInDegrees()
    //{

    //    float localEulerRotationX = transform.localRotation.eulerAngles.x;
    //    float localEulerRotationY = transform.localRotation.eulerAngles.y;
    //    float localModelEulerRotationY = localModelRotation.eulerAngles.y;

    //    //accounting for Euler taking long way around
    //    if (localEulerRotationY > 180) localEulerRotationY = 360 - localEulerRotationY;
    //    if (localEulerRotationX > 70) localEulerRotationX = 360 - localEulerRotationX;
    //    if (localModelEulerRotationY > 180) localModelEulerRotationY = 360 - localModelEulerRotationY;

    //    float changeInRotationY = Mathf.Abs(localEulerRotationY - localModelEulerRotationY);
    //    //calculating total change in rotation (idk why it's scuffed pythagoras but it works)
    //    float totalRotation = Mathf.Sqrt(localEulerRotationX + localEulerRotationX + changeInRotationY + changeInRotationY);


    //    return totalRotation;
    //}
    #endregion
    public void onLook(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}
