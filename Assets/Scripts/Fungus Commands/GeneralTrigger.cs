using Cinemachine;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CommandInfo("Scripting",
                 "General Move Trigger",
                 "General moves to Corkboard")]
[AddComponentMenu("")]

public class GeneralTrigger : Command
{
    [Tooltip("General Model")]
    [SerializeField] protected GeneralBounce GeneralModel;
    public override void OnEnter()
    {
        base.OnEnter();
        GeneralModel.StartMove();
    }

}
