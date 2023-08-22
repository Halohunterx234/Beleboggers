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
    NavMeshAgent agent;
    Animator anim;
    public Transform target;
    public LayerMask friendlies, obstacles;

    [Header("Field of Vision")]
    public float fovRadius, fovAngle;
    public List<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //reset list of targets
        targets.Clear();

        //animations
        //anim.SetFloat("MoveY", agent.velocity.magnitude / agent.desiredVelocity.magnitude);

        //Field of vision
        Collider[] colliders = Physics.OverlapSphere(transform.position, fovRadius, friendlies);

        //Go through all friendlies in the fov range
        foreach (Collider collider in colliders)
        {
            Vector3 distanceToGameObject = collider.gameObject.transform.position - transform.position;

            //check if its within fov angle
            if (Vector3.Angle(transform.forward, distanceToGameObject) <= fovAngle * 0.5f)
            {
                //check if got vision and not being blocked by obstacles
                bool canSee = Physics.Raycast(transform.position + 0.5f*Vector3.up, (collider.gameObject.transform.position - this.transform.position).normalized, Mathf.Infinity, ~obstacles);
                if (canSee)
                {
                    targets.Add(collider.gameObject);
                }
            }
        }

        //Sort to find closest target
        float distance = float.PositiveInfinity;
        GameObject closestFriendly = null;

        foreach (GameObject go in targets)
        {
            float newdistance = Vector3.Distance(go.transform.position, this.transform.position);
            if (newdistance < distance)
            {
                distance = newdistance;
                closestFriendly = go;
            }
        }

        //Find the closest target
        if (closestFriendly != null)
        {
            Transform friendlyTarget = closestFriendly.transform;
            agent.SetDestination(friendlyTarget.position);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }
}