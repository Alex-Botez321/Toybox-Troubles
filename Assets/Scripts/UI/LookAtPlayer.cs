using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;
    float lockPos = 0;
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
        transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
