using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float up;
    public float speed = 1.0f;
    public float angle = 20.0f;

    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y + up, target.transform.position.z));

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            offset = Quaternion.AngleAxis(90.0f * Time.deltaTime, Vector3.up) * offset;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            offset = Quaternion.AngleAxis(-90.0f * Time.deltaTime, Vector3.up) * offset;
        }
    }
}
