using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceInteract : MonoBehaviour
{
    [SerializeField]private Flowchart flowchart;
    public VariableReference pickedUp;

    public void CollectEvidence()
    {
        Debug.Log("there is something here");
        flowchart.enabled = true;
        flowchart.ExecuteBlock("say block");

    }
}
