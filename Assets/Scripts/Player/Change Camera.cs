using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    private CinemachineVirtualCamera fixedCamera;

    private void Awake()
    {
        fixedCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fixedCamera.Priority = 100;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            fixedCamera.Priority = 0;

        }
    }
}
