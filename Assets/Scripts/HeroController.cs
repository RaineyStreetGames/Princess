using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    public float runSpeed = 10.0f;
    public float walkSpeed = 3.0f;
    private Vector3 origin;

    private Animator animator;
    private NavMeshAgent agent;

    public void Reset()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        Reset();
    }

    // DEBUG
    void OnDrawGizmos()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     RaycastHit? hit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f).FirstOrDefault(x => x.transform.tag == "Terrain");
        //     if (hit.HasValue && hit.Value.point != Vector3.zero)
        //     {
        //         Gizmos.DrawSphere(hit.Value.point, 0.5f);
        //         Gizmos.DrawSphere(transform.position + ((hit.Value.point - transform.position).normalized * 3), 1.0f);
        //         Debug.DrawLine(transform.position, hit.Value.point, Color.white, 2.0f);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse Drag Event
        if (Input.GetMouseButton(0))
        {
            RaycastHit? hit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f).FirstOrDefault(x => x.transform.tag == "Terrain");
            if (hit.HasValue && hit.Value.point != Vector3.zero)
            {
                if (Vector3.Distance(transform.position, hit.Value.point) < agent.stoppingDistance)
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Walk", true);
                    agent.speed = walkSpeed;
                }
                else
                {
                    var direction = (hit.Value.point - transform.position).normalized;
                    var destination = transform.position + direction * 3;
                    NavMeshHit navHit;
                    if (NavMesh.SamplePosition(destination, out navHit, 5.0f, NavMesh.AllAreas))
                    {
                        // Debug.Log($"Ray: {hit.Value.point} Nav {navHit.position}");
                        agent.SetDestination(destination);
                        agent.speed = runSpeed;
                        transform.forward = direction;
                        animator.SetBool("Run", true);
                        animator.SetBool("Walk", false);
                    }
                }
            }
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetTrigger("Roll");
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.SetTrigger("Attack");
        }
    }
}
