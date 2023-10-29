using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

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
        if (Mouse.current.leftButton.isPressed)
        {
            RaycastHit? hit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), 100.0f).FirstOrDefault(x => x.transform.tag == "Terrain");
            if (hit.HasValue && hit.Value.point != Vector3.zero)
            {
                Gizmos.DrawSphere(hit.Value.point, 0.5f);
                Gizmos.DrawSphere(transform.position + ((hit.Value.point - transform.position).normalized * 3), 1.0f);
                Debug.DrawLine(transform.position, hit.Value.point, Color.white, 2.0f);
            }
        }
    }

    void Update()
    {

        // if (Gamepad.current != null)
        // {

        //     float horizontalInput = Gamepad.current.leftStick.ReadValue().x;
        //     // Debug.Log("horizontal " + horizontalInput);
        //     float verticalInput = Gamepad.current.leftStick.ReadValue().y;
        //     // Debug.Log("vertical " + verticalInput);

        //     Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);


        //     Debug.Log("forward " + transform.forward);
        //     if (!movement.Equals(Vector3.zero))
        //     {
        //         // Joystick Event
        //         if (movement.magnitude > agent.stoppingDistance)
        //         {
        //             animator.SetBool("Run", true);
        //             animator.SetBool("Walk", false);
        //             agent.speed = runSpeed;
        //         }
        //         else if (movement.magnitude > agent.stoppingDistance * 2)
        //         {
        //             animator.SetBool("Run", false);
        //             animator.SetBool("Walk", true);
        //             agent.speed = walkSpeed;
        //         }


        //         Debug.Log("movement " + movement + " magnitude " + movement.magnitude);
        //         var direction = (transform.forward * verticalInput) + (transform.right * (horizontalInput));
        //         var destination = transform.position + direction.normalized;
        //         Debug.Log("forward " + transform.forward);
        //         // Debug.Log("direction " + direction);
        //         // Debug.Log("destination " + destination);
        //         // agent.Move(movement * Time.deltaTime * agent.speed);
        //         agent.SetDestination(destination);
        //         // transform.forward = transform.forward + Vector3.ClampMagnitude(transform.right * (horizontalInput / 25), agent.speed);
        //         // transform.Transla

        //     }
        // }
        // if (Mouse.current.leftButton.isPressed && !EventSystem.current.IsPointerOverGameObject())
        // {
        //     // Mouse Drag Event
        //     RaycastHit? hit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), 100.0f).FirstOrDefault(x => x.transform.tag == "Terrain");
        //     if (hit.HasValue && hit.Value.point != Vector3.zero)
        //     {
        //         if (Vector3.Distance(transform.position, hit.Value.point) < agent.stoppingDistance)
        //         {
        //             animator.SetBool("Run", false);
        //             animator.SetBool("Walk", true);
        //             agent.speed = walkSpeed;
        //         }
        //         else
        //         {
        //             var direction = (hit.Value.point - transform.position).normalized;
        //             var destination = transform.position + direction * 3;
        //             NavMeshHit navHit;
        //             if (NavMesh.SamplePosition(destination, out navHit, 5.0f, NavMesh.AllAreas))
        //             {
        //                 // Debug.Log($"Ray: {hit.Value.point} Nav {navHit.position}");
        //                 agent.SetDestination(destination);
        //                 agent.speed = runSpeed;
        //                 transform.forward = direction;
        //                 animator.SetBool("Run", true);
        //                 animator.SetBool("Walk", false);
        //             }
        //         }
        //     }
        // }

        // if (agent.remainingDistance <= agent.stoppingDistance)
        // {
        //     animator.SetBool("Run", false);
        //     animator.SetBool("Walk", false);
        // }
    }

    public void Move(InputAction.CallbackContext context)
    {
        // if (context.started)
        //     Debug.Log("Action was started");
        // else if (context.performed)
        //     Debug.Log("Action was performed");
        var movement = context.ReadValue<Vector2>();
        if (movement.magnitude > agent.stoppingDistance)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            agent.speed = runSpeed;
        }
        else if (movement.magnitude > agent.stoppingDistance * 2)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);
            agent.speed = walkSpeed;
        }


        Debug.Log("movement " + movement + " magnitude " + movement.magnitude);
        var direction = (transform.forward * movement.y) + (transform.right * (movement.x));
        var destination = transform.position + direction * 3;
        agent.SetDestination(destination);

        if (context.canceled)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse Drag Event
            RaycastHit? hit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), 100.0f).FirstOrDefault(x => x.transform.tag == "Terrain");
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

        if (context.canceled)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        animator.SetTrigger("Attack");
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        animator.SetTrigger("Roll");
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        animator.SetTrigger("Jump");
    }
}
