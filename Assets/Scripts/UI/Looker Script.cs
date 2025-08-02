using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LookerScript : MonoBehaviour
{
    [SerializeField] private Image LookerIcon;
    [SerializeField] public GameObject InDialogueScreen;

    [SerializeField] private Flowchart flowchart;
    [SerializeField] private Flowchart woodFlowchart;
    [SerializeField] private Flowchart patchFlowchart;
    [SerializeField] private Flowchart banksIntroFlowchart;
    [SerializeField] private Flowchart coinsFlowchart;

    [SerializeField] private bool IsTextActive = false;
    private bool backgroundIsActive = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //LookerIcon.SetActive(true);
        LookerIcon = GetComponent<Image>();
        LookerIcon.enabled = true; 
    }

    public void Update()
    {
        CheckForBackground();
        if (flowchart.HasExecutingBlocks())
        {
            IsTextActive = true;
            LookerIcon.enabled = false;

        }
        else if (backgroundIsActive == true)
        {
            IsTextActive = true;
            LookerIcon.enabled = false;
        }
        else
        {
            IsTextActive= false;
            LookerIcon.enabled = true;
        }
    }

    public void OnHintInput(InputAction.CallbackContext context)
    {
        Debug.Log("RAAAAAAAH");
        if (IsTextActive == false && LookerIcon.enabled)
        {
            OnLoookerAskHint();

        }
    }

    private void OnLoookerAskHint()
    {
        //int 0 to 3 set as counter, corresponding to each 
        int counter = Random.Range(0, 4);
        flowchart.SetIntegerVariable("Counter", counter);


        if (patchFlowchart.GetBooleanVariable("FoundSingularPatch") == true)
        {
            //patch found = true    //0
            flowchart.SetBooleanVariable("PP_PatchFound", true);
        }
        if (woodFlowchart.GetBooleanVariable("FoundWood") == true)
        {
            //wood found = true     //1
            flowchart.SetBooleanVariable("Bill_WoodFound", true);
        }
        if (coinsFlowchart.GetBooleanVariable("CoinsFound") == true)
        {
            //coins found = true    //2
            flowchart.SetBooleanVariable("Banks_CoinsFound", true);
        }
        if (coinsFlowchart.GetBooleanVariable("NotesFound") == true)
        {
            //coins found = true    //3
            flowchart.SetBooleanVariable("NotesFound", true);
        }

        //BANDAID FIX DONT LOOOOK
        if (flowchart.GetVariable("Banks_CoinsFound") == true 
            && flowchart.GetVariable("PP_PatchFound") == true
            && flowchart.GetVariable("Bill_WoodFound") == true
            && flowchart.GetVariable("NotesFound") == false)
        {
            flowchart.SetBooleanVariable("bandaidFix", true);
        }


        //flowchart.enabled = true;
        //if (flowchart.GetBooleanVariable("PassedTutorial"))
        //{
        //    PassedTutorial=true;
        //}


        //we could change this to be a switch statement when we get to the real case
        //atm im not sure how far ill go in this built, im not sure this is the best way to go about
        //how the hint system works because the game is nonlinear, and this is a linear line of questions


        flowchart.ExecuteBlock("say block");
    }

    private void CheckForBackground()
    {
        for(int i = 0; i < InDialogueScreen.transform.childCount; i++)
        {
            if(InDialogueScreen.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                backgroundIsActive = true;
                Debug.Log(InDialogueScreen.transform.GetChild(i));
                break;
            }
            else
            {
                backgroundIsActive = false;
            }
        }
    }
}
