using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : Entity
{
    NavMeshAgent agent;
    Animator anim;
    public Transform target;
    public LayerMask player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        
        //anim.SetFloat("MoveY", agent.velocity.magnitude / agent.desiredVelocity.magnitude);
    }
}