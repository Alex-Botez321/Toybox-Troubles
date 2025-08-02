using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJumpData : MonoBehaviour
{
    [SerializeField]public float jumpSpeed = 10;
    [SerializeField]public Transform targetPosTransform;
    [SerializeField]public float arcHeight = 1;

    public Vector3 getTargetPos()
    {
        Vector3 targetPos = targetPosTransform.position;
        return targetPos;
    }
}
