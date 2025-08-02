using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] private Flowchart flowchart;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("npc is talking");
        flowchart.enabled = true;
        flowchart.ExecuteBlock("say block");

        while (flowchart.HasExecutingBlocks())
        {
            PlayerController playerController = GetComponent<PlayerController>();
            playerController.moveDirection = Vector3.zero;
        }
    }
}
