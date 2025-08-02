using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    //[SerializeField] PlayerController playerController;
    //[SerializeField] private float minimumSpeed = 2f;
    //[SerializeField] private float speedChange = 0.01f;
    //private float maxSpeed;
    //void Start()
    //{
    //    maxSpeed = playerController.speed;
    //}
    //private void FixedUpdate()
    //{
    //    if (playerController.speed > minimumSpeed)
    //    {
    //        playerController.speed -= speedChange;
    //    }
    //    else
    //        playerController.speed = minimumSpeed;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        StartCoroutine(SpeedIncrementDown());
    //    }
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        StartCoroutine(SpeedIncrementUp());
    //    }
    //}

    //IEnumerator SpeedIncrementDown()
    //{
    //    StopCoroutine(SpeedIncrementUp());
    //    if (playerController.speed > minimumSpeed)
    //    {
    //        playerController.speed -= speedChange;
    //        StartCoroutine(SpeedIncrementDown());
    //    }
    //    else
    //        playerController.speed = minimumSpeed;

    //    yield return null;
    //}

    //IEnumerator SpeedIncrementUp()
    //{
    //    StopCoroutine(SpeedIncrementDown());
    //    if (playerController.speed < maxSpeed)
    //    {
    //        playerController.speed += speedChange;
    //        StartCoroutine(SpeedIncrementUp());
    //    }
    //    else
    //        playerController.speed = maxSpeed;

    //    yield return null;
    //}

}
