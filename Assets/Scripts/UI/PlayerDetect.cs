using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] private GameObject interactUI;

    private void OnTriggerEnter(Collider other)
    {
        interactUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactUI.SetActive(false);
    }
}
