using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLineRenderer : MonoBehaviour
{
    private int curveSmoothness = 20;

    private Vector3 p0 = Vector3.zero;
    private Vector3 p1 = Vector3.zero;
    private Vector3 p2 = Vector3.zero;
    private Vector3 p3 = Vector3.zero;

    private LineRenderer lineRenderer;
    private GameObject player;
    private BezierJump bezierJump;
    [SerializeField] private Vector3[] bezierVectors; //Array of Vector3s along curve

    // Start is called before the first frame update
    void Start()
    {
        // Get location of control points
        p0 = this.gameObject.transform.GetChild(0).position;
        p1 = this.gameObject.transform.GetChild(1).position;
        p2 = this.gameObject.transform.GetChild(2).position;
        p3 = this.gameObject.transform.GetChild(3).position;
        lineRenderer = GetComponent<LineRenderer>();
        bezierJump = GetComponentInParent<BezierJump>();
        player = bezierJump.player;
        
        lineRenderer.enabled = false;
    }



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

            lineRenderer.SetPosition(index, bezierVectors[index]);

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



            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(prevPoint, bezierVectors[index]);

            prevPoint = bezierVectors[index];
            index++;

        }
    }

}
