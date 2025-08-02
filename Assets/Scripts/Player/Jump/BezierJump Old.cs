using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierJumpOld : MonoBehaviour
{
    private int curveSmoothness = 20;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3[] bezierVectors = new Vector3[20];

    private PlayerController playerController;

    private int i = 0;
    private bool canJump = false;

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public float jumpSpeed = 5;

    public Texture2D tex;
    private Rect rect;

    private MeshRenderer endMarkerMesh;

    void Start()    
    {
        p0 = this.gameObject.transform.GetChild(0).position;
        p1 = this.gameObject.transform.GetChild(1).position;
        p2 = this.gameObject.transform.GetChild(2).position;
        p3 = this.gameObject.transform.GetChild(3).position;

        float center = Screen.width / 2.0f;
        rect = new Rect(center - 200, 50, tex.width, tex.height);
        playerController = player.GetComponent<PlayerController>();

        endMarkerMesh = this.gameObject.transform.GetChild(3).GetChild(0).GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Get bezier Vector3s along curve
    /// </summary>
    /// <returns>Array Of Vector3s</returns>
    public Vector3[] getPoints() { calculatePoints();  return bezierVectors; }

    /// <summary>
    /// Sets up the variables for the jump
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        /*
         * possible ideas:
         * chain jumps as child objects of the original, set up array size based on amount of children
         * figure something out with force
         * 
         */
        if (other.tag == "Player")
        {
            canJump = true;
            endMarkerMesh.enabled = true;

            playerController.canJump = true;
            
            //playerController.jumpSpeed = jumpSpeed;
            //p0 = player.transform.position;
            //Debug.Log(gameObject.name);
            calculatePoints();
                playerController.jumpPoint = 0;
                //playerController.jumpFraction = 0;
            //if (playerController.jumpPoint >= 14 ) 
            //{
            //    //Debug.Log("Reset");
            //    //p0 = player.transform.position;
            //    playerController.ResetJump();
                
            //}
            //playerController.jumpCoords = bezierVectors;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canJump = false;
            endMarkerMesh.enabled = false;
            i = 0;
        }
    }

    private void calculatePoints()
    {
        i = 0;
        p0 = player.transform.position;
        Debug.Log(p0);
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

            bezierVectors[i].x = (float)x;
            bezierVectors[i].y = (float)y;
            bezierVectors[i].z = (float)z;

            i++;


        }
    }

    void OnGUI()
    {
        if(canJump)
            GUI.DrawTextureWithTexCoords(rect, tex, new Rect(0.0f, 0.0f, 1f, 1f));
    }

    /// <summary>
    /// Drawing Bezier Curve in Editor
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        p0 = this.gameObject.transform.GetChild(0).position;
        p1 = this.gameObject.transform.GetChild(1).position;
        p2 = this.gameObject.transform.GetChild(2).position;
        p3 = this.gameObject.transform.GetChild(3).position;
        int i = 0;
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

            bezierVectors[i].x = (float)x;
            bezierVectors[i].y = (float)y;
            bezierVectors[i].z = (float)z;

            

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(prevPoint, bezierVectors[i]);

            prevPoint = bezierVectors[i];
            i++;

        }
    }

}

