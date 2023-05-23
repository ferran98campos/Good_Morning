using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ShadowPersonMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public Animator animator;
    public GameObject failureWindow;

    public float distance = 2f;
    public float duration = 2f;
    private bool capturePlayer = false;
    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        float disNPC2Player = Vector3.Distance(transform.position, target.position);
        if (disNPC2Player <= distance && !capturePlayer)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                failureWindow.SetActive(true);
                capturePlayer = true;
            }
        }
        else
        {
            timer = 0f;
        }
        
        agent.SetDestination(target.position);

        // set parameters for the animator
        animator.SetFloat("Horizontal", agent.velocity.x);
        animator.SetFloat("Vertical", agent.velocity.y);
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
