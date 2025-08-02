using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private TagAttribute groundLayer;
    public bool isGrounded;
    [SerializeField] private Rigidbody playerRb;
    private int triggerCounter = 0;

    private void Awake()
    {
        //playerRb = GetComponentInParent<Rigidbody>();
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Ground")
        {
            triggerCounter++;
            if (triggerCounter >= 0)
                playerRb.drag = 12;

            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Ground")
        {
            triggerCounter--;
            if (triggerCounter <= 0)
                playerRb.drag = 0;
        }
    }
}
