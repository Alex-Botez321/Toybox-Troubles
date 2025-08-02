using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskerRayCast : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float rayDistance = 5;
    private void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;

        Vector3 rayDirection = transform.TransformDirection(Vector3.back);


        if (Physics.Raycast(transform.position, rayDirection, out hit, rayDistance, layerMask))
        {
            Debug.DrawRay(transform.position, rayDirection * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.white);
        }
    }
}
