using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("Run", false);
        }

        // Mouse Drag Event
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform.tag == "Terrain")
            {
                agent.SetDestination(hit.point);
                transform.forward = hit.point - transform.position;
                animator.SetBool("Run", true);
            }
            // Debug.Log($"Drag {hit.transform.name} @ {hit.point}");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        // Mouse Up Event
        // if (Input.GetMouseButtonUp(0))
        // {
        // }
    }
}
