using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float up;
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;
    public float smooth = 10.0f;
    private float distance;

    void Start()
    {
        offset = offset.normalized;
        distance = maxDistance;
    }

    void LateUpdate()
    {
        Vector3 desiredCameraPos = target.transform.position + (offset * maxDistance);
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            offset = Quaternion.AngleAxis(90.0f * Time.deltaTime, Vector3.up) * offset;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            offset = Quaternion.AngleAxis(-90.0f * Time.deltaTime, Vector3.up) * offset;
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit? tilt = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition)).FirstOrDefault(x => x.transform.tag == "Terrain");
            if (tilt.HasValue)
            {
                var playerAngle = target.transform.position - tilt.Value.point;
                var cameraAngle = transform.position - tilt.Value.point;
                offset = Quaternion.AngleAxis(Vector3.SignedAngle(cameraAngle, playerAngle, Vector3.up) * Time.deltaTime, Vector3.up) * offset;
            }
        }
    }
}
