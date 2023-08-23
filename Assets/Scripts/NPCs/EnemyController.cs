using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : Entity
{

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        //animations
        //anim.SetFloat("MoveY", agent.velocity.magnitude / agent.desiredVelocity.magnitude);

        //FOV
        FieldOfVisionCheck(friendlies);

        //Check Attack
        UpdateAtkCD();
    }

    //colliders
    private void OnTriggerEnter(Collider other)
    {
        if (CurrentTarget == null || other.gameObject.GetComponent<EnemyController>()) return;
        //Check if it is a entity, and it is the same target the enemy has been chasing
        if (CurrentTarget.GetComponent<Entity>() != null && CurrentTarget.gameObject == other.gameObject)
        {
            if (canAtk)
            {
                Attack(other, friendlies);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (CurrentTarget == null || other.gameObject.GetComponent<EnemyController>()) return;
        //Check if it is a entity, and it is the same target the enemy has been chasing
        if (CurrentTarget.GetComponent<Entity>() != null && CurrentTarget.gameObject == other.gameObject)
        {
            if (canAtk)
            {
                Attack(other, friendlies);
            }
        }
    }
}