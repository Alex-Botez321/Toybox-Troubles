using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GeneralBounce : MonoBehaviour
{
    [SerializeField] private float lerpMoveTime = 7f;
    [SerializeField] private float lerpRotateTime = 1f;
    [SerializeField] private float bounceSpeed = 0.00005f;
    private Vector3 generalStartPosition;
    private Quaternion generalStartRotate;
    private Vector3 generalEndPosition = new Vector3(-0.0191f, 0.0023f, 0.0099f);
    private Vector3 generalEndRotateEuler = new Vector3 (-3.055f, -90f, -90f);
    private Quaternion generalEndRotation = Quaternion.identity;
    private float localJumpPeak = -0.009986294f;
    private float localJumpDip = -0.0308f;

    private void Awake()
    {
        generalStartPosition = transform.localPosition;
        generalStartRotate = transform.localRotation;
        generalEndRotation = Quaternion.Euler(generalEndRotateEuler);
    }

    public void StartMove()
    {
        StartCoroutine(MoveLerp());
    }

    IEnumerator MoveLerp()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpMoveTime)
        {
            Vector2 currentPosition = Vector2.Lerp(new Vector2(generalStartPosition.x, generalStartPosition.y),
                                                   new Vector2 (generalEndPosition.x, generalEndPosition.y), 
                                                   timeElapsed / lerpMoveTime);

            transform.localPosition = new Vector3(currentPosition.x, currentPosition.y, transform.localPosition.z + bounceSpeed);

            if (transform.localPosition.z > localJumpPeak)
            {
                bounceSpeed *= -1; 
                

            }
            else if (transform.localPosition.z < localJumpDip)
            {
                bounceSpeed *= -1;
                
            }

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        StartCoroutine(RotateLerp());
    }

    IEnumerator RotateLerp()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpRotateTime)
        {
            transform.localRotation = Quaternion.Lerp(generalStartRotate, generalEndRotation, timeElapsed / lerpRotateTime);
            timeElapsed += Time.deltaTime;

            if(transform.localPosition.z < localJumpPeak)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + Mathf.Abs(bounceSpeed));

            yield return null;
        }
    }
}
