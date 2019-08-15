using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum HeroState
{
    Inactive,
    Active,
}

public class Hero : MonoBehaviour
{
    public HeroState state = HeroState.Inactive;
    public float wanderingMax = 10.0f;
    public float runSpeed = 10.0f;
    public float walkSpeed = 1.5f;

    private bool waiting;
    private bool wandering;
    private float timer;
    private Vector3 origin;
    private Animator animator;
    private NavMeshAgent agent;

    public void Reset()
    {
        waiting = false;
        wandering = false;
        timer = 0.0f;
        agent.SetDestination(transform.position);
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case HeroState.Inactive:
                InactiveUpdate();
                break;
            case HeroState.Active:
                ActiveUpdate();
                break;
        }

    }

    protected void ActiveUpdate()
    {
        // Mouse Drag Event
        if (Input.GetMouseButton(0))
        {
            RaycastHit? hit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f).FirstOrDefault(x => x.transform.tag == "Terrain");
            if (hit.HasValue && hit.Value.point != Vector3.zero)
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hit.Value.point, out navHit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(navHit.position);
                    agent.speed = runSpeed;
                    transform.forward = navHit.position - transform.position;
                    animator.SetBool("Run", true);
                }
            }
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("Run", false);
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

    protected void InactiveUpdate()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("Walk", false);
            wandering = false;
        }

        if (waiting && !wandering)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                var randX = Random.Range(-10.0f, 10.0f);
                var randZ = Random.Range(-10.0f, 10.0f);
                var target = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
                agent.SetDestination(target);
                agent.speed = walkSpeed;
                transform.forward = target - transform.position;
                animator.SetBool("Walk", true);
                waiting = false;
                wandering = true;
            }
        }
        else
        {
            timer = Random.Range(1.5f, 4.5f);
            waiting = true;
        }
    }
}
