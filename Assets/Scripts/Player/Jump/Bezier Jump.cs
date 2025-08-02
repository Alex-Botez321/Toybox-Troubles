using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierJump : MonoBehaviour
{
    public const int curveSmoothness = 20; //How many Vector3s to calculate along curve
    [SerializeField] private Vector3[] bezierVectors; //Array of Vector3s along curve
    [SerializeField] private float timeToCompleteJump = 1f;
    [SerializeField] private bool isChainable = false;
    private float timePerSegment;
    private LineRenderer lineRenderer;
    private JumpLineRenderer lineRenderScript;

    [SerializeField] public GameObject player;
    private PlayerController playerController;

    private Vector3 p0 = Vector3.zero;
    private Vector3 p1 = Vector3.zero;
    private Vector3 p2 = Vector3.zero;
    private Vector3 p3 = Vector3.zero;

    private GameObject jumpPrompt;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderScript = GetComponentInChildren<JumpLineRenderer>();
        //set up variables for player
        if (player == null)
            Debug.Log("<color=red>Error: </color>Player reference missing in " + gameObject.name + "Bezier Jump component");
        else
            playerController = player.GetComponent<PlayerController>();
        timePerSegment = timeToCompleteJump / curveSmoothness;

        // Get location of control points
        p0 = this.gameObject.transform.GetChild(0).position;
        p1 = this.gameObject.transform.GetChild(1).position;
        p2 = this.gameObject.transform.GetChild(2).position;
        p3 = this.gameObject.transform.GetChild(3).position;

        lineRenderer.enabled = false;

        jumpPrompt = this.gameObject.transform.GetChild(5).gameObject;
    }

    /// <summary>
    /// Gives the player controller the data needed for the jump
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if((isChainable && playerController.jumpPoint > 5) || (!isChainable && playerController.jumpPoint < 5))
            {
                if(playerController.currentJump != this.gameObject)
                {
                   
                    playerController.jumpPoint = 0;
                    playerController.currentJump = this.gameObject;
                    playerController.jumpCoords = bezierVectors;
                    playerController.maxTime = timePerSegment;
                    playerController.canJump = true;
                    
                }

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            if(!isChainable)
            {
                CalculatePoints();
                lineRenderScript.CalculatePoints();
                lineRenderer.enabled = true;
                jumpPrompt.SetActive(true);
            }

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            lineRenderer.enabled = false;
            playerController.canJump = false;
            playerController.currentJump = null;
            jumpPrompt.SetActive(false);
        }
    }

    /// <summary>
    /// Calculate all of the points along the bezier curve
    /// </summary>
    public void CalculatePoints()
    {
        int index = 0;
        p0 = player.transform.position;
        for (float percent = 0f; percent <= 1f; percent += 1.0f / curveSmoothness)
        {
            double x = Math.Pow(1 - percent, 3) * p0.x +
                3 * Math.Pow(1 - percent, 2) * percent * p1.x +
                3 * (1 - percent) * Math.Pow(percent, 2) * p2.x +
                Math.Pow(percent, 3) * p3.x;

            double y = Math.Pow(1 - percent, 3) * p0.y +
                3 * Math.Pow(1 - percent, 2) * percent * p1.y +
                3 * (1 - percent) * Math.Pow(percent, 2) * p2.y +
                Math.Pow(percent, 3) * p3.y;

            double z = Math.Pow(1 - percent, 3) * p0.z +
                3 * Math.Pow(1 - percent, 2) * percent * p1.z +
                3 * (1 - percent) * Math.Pow(percent, 2) * p2.z +
                Math.Pow(percent, 3) * p3.z;

            bezierVectors[index].x = (float)x;
            bezierVectors[index].y = (float)y;
            bezierVectors[index].z = (float)z;

            

            index++;


        }
    }

    /// <summary>
    /// Drawing Bezier Curve in Editor
    /// </summary>
    private void OnDrawGizmos()
    {
        p0 = this.gameObject.transform.GetChild(0).position;
        p1 = this.gameObject.transform.GetChild(1).position;
        p2 = this.gameObject.transform.GetChild(2).position;
        p3 = this.gameObject.transform.GetChild(3).position;
        int index = 0;
        Vector3 prevPoint = p0;
        for (float percent = 0f; percent < 1f; percent += 1.0f / curveSmoothness)
        {
            double x = Math.Pow(1 - percent, 3) * p0.x +
                3 * Math.Pow(1 - percent, 2) * percent * p1.x +
                3 * (1 - percent) * Math.Pow(percent, 2) * p2.x +
                Math.Pow(percent, 3) * p3.x;

            double y = Math.Pow(1 - percent, 3) * p0.y +
                3 * Math.Pow(1 - percent, 2) * percent * p1.y +
                3 * (1 - percent) * Math.Pow(percent, 2) * p2.y +
                Math.Pow(percent, 3) * p3.y;

            double z = Math.Pow(1 - percent, 3) * p0.z +
                3 * Math.Pow(1 - percent, 2) * percent * p1.z +
                3 * (1 - percent) * Math.Pow(percent, 2) * p2.z +
                Math.Pow(percent, 3) * p3.z;

            bezierVectors[index].x = (float)x;
            bezierVectors[index].y = (float)y;
            bezierVectors[index].z = (float)z;



            Gizmos.color = Color.blue;
            Gizmos.DrawLine(prevPoint, bezierVectors[index]);

            prevPoint = bezierVectors[index];
            index++;

        }
    }
}