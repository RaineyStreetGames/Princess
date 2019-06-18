using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    public float speed = 5.0f;
    // public Animator animator;

    private Animator animator;
    private Vector3? targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition.HasValue)
        {

            // Debug.Log("has value");
            // rotate towards the targetPosition


            // move towards the targetPosition
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.Value, speed * Time.deltaTime);

            if (transform.localPosition == targetPosition)
            {
                targetPosition = null;
                animator.SetTrigger("Stop");
            }
        }

        // Mouse Drag Event
        // if (Input.GetMouseButton(0)) {
        // }

        // Mouse Up Event
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform.tag == "Terrain")
            {
                targetPosition = hit.point;
                transform.forward = hit.point - transform.position;
                animator.SetTrigger("Run");
            }
            Debug.Log($"Clicked {hit.transform.name} @ {hit.point}");
        }
    }
}
