using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public GameObject player;
    public float cameraHeight = 7.0f;
    public float cameraDepth = 14.0f;

    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        pos.z -= cameraDepth;
        transform.position = pos;
    }
}
