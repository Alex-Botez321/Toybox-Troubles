using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField]private float interactRange = 1f;

    /// <summary>
    /// On the input of the "E" button, an array of colliders is created
    /// the array contains all objects that overlap with a physics sphere object attached to the player,
    /// we then run through the array to check if any of the objects in the array contain the 
    /// "NPCIntraction" script componenet
    /// </summary>

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        Collider[] InteractableCollidersArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider Interactable in  InteractableCollidersArray)
        {
            if(Interactable.TryGetComponent(out NPCInteraction npcInteraction))
            {
                npcInteraction.Talk();
            }
            if(Interactable.TryGetComponent(out EvidenceInteract evidenceInteract))
            {
                evidenceInteract.CollectEvidence();
            }
        }
    }

}


