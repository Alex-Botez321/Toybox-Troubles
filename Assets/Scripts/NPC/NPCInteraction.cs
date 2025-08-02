using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField]private Flowchart flowchart;


    public void Talk()
    {
        Debug.Log("npc is talking");
        flowchart.enabled = true;
        flowchart.ExecuteBlock("say block");

        while(flowchart.HasExecutingBlocks())
        {
            PlayerController playerController = GetComponent<PlayerController>();
            playerController.moveDirection = Vector3.zero;
        }
    }

}
