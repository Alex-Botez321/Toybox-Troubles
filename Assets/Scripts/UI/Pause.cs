using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
    private PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        interactUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Flowchart.BroadcastFungusMessage(pickupMessage);
        interactUI.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interactUI.SetActive(true);
            playerController.InteractOn();
        }
    }
}
