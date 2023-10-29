using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMain : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float up;
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;
    public float smooth = 10.0f;
    private float distance;

    private Vector3 lastTarget;
    private Vector3 lastLastTarget;

    void Start()
    {
        offset = offset.normalized;
        distance = maxDistance;
        lastTarget = target.transform.position;
        lastLastTarget = target.transform.position;
    }

    // DEBUG
    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawRay(new Ray(target.transform.position, transform.position.xz() - target.transform.position.xz()));
    //     Gizmos.DrawRay(new Ray(target.transform.position, lastLastTarget.xz() - target.transform.position.xz()));
    // }

    void LateUpdate()
    {
        Vector3 desiredCameraPos = target.transform.position + (offset * maxDistance * 2);
        RaycastHit hit;
        if (Physics.Linecast(target.transform.position, desiredCameraPos, out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.87f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        Vector3 obstructedCameraPos = target.transform.position + (offset * distance);
        transform.position = Vector3.Lerp(transform.position, new Vector3(obstructedCameraPos.x, maxDistance, obstructedCameraPos.z), Time.deltaTime * smooth);
        transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y + up, target.transform.position.z));

        // Rotate camera.
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            offset = Quaternion.AngleAxis(90.0f * Time.deltaTime, Vector3.up) * offset;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            offset = Quaternion.AngleAxis(-90.0f * Time.deltaTime, Vector3.up) * offset;
        }

        var angle = Vector3.SignedAngle(transform.position.xz() - target.transform.position.xz(), lastTarget.xz() - target.transform.position.xz(), Vector3.up);

        var speed = 2f;
        if (Mathf.Abs(angle) > 160)
        {
            speed = 3.5f;
        }
        else if (Mathf.Abs(angle) > 60)
        {
            speed = 2.5f;
        }
        offset = Quaternion.AngleAxis(angle * Time.deltaTime * speed, Vector3.up) * offset;

        lastLastTarget = lastTarget;
        lastTarget = target.transform.position;

    }
}
