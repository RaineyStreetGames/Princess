using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(target.transform);
    }
}
