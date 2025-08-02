using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private CinemachineBrain cameraBrain;
    [SerializeField] private GameObject playerCamera;

    // Update is called once per frame
    void Update()
    {
        GameObject activeCamera = cameraBrain.ActiveVirtualCamera.VirtualCameraGameObject;
        if (activeCamera == playerCamera)
            transform.rotation = Quaternion.Euler(0, cameraTarget.rotation.eulerAngles.y, 0);
        else
            transform.rotation = Quaternion.Euler(0, activeCamera.transform.rotation.eulerAngles.y, 0);
    }
}
