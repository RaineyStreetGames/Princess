using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cloud : MonoBehaviour
{
    private float speed;

    private Rigidbody rb;

    public void Init(float speed)
    {
        this.speed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;

        if (transform.position.x < -CloudController.maxX)
        {
            transform.position = new Vector3(CloudController.maxX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > CloudController.maxX)
        {
            transform.position = new Vector3(-CloudController.maxX, transform.position.y, transform.position.z);
        }
        else if (transform.position.z < -CloudController.maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, CloudController.maxZ);
        }
        else if (transform.position.z > CloudController.maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -CloudController.maxZ);
        }
    }
}
