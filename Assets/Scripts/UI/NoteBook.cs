using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBook : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private GameObject interactUI2;
    [SerializeField] private GameObject New;
    void Start()
    {
        Player = GameObject.Find("Player");
        interactUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!interactUI.activeSelf)
            {
                New.SetActive(false);
                interactUI.SetActive(true);
                interactUI2.SetActive(false);
                Player.GetComponent<PlayerController>().isInteracting = true;
                Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else
            {
                interactUI.SetActive(false);
                interactUI2.SetActive(true);
                Player.GetComponent<PlayerController>().isInteracting = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Flowchart.BroadcastFungusMessage(pickupMessage);
        interactUI.SetActive(true);
    }
}
