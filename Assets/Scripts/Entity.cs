using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

//base class for all entities that have hp
public class Entity : MonoBehaviour
{
    //essential stuff
    public int hp;

    //Animator
    protected Animator anim;


    //audio 
    public AudioSource source;
    public AudioClip clip;
    AudioSource aud;
    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    //~~AI~~
    [Header("navMesh")]
    public Transform flagORplayer;
    NavMeshAgent agent;
    [Range(0f, 10f)]
    public float lockOnDistance = 1; //the distacne where entities will not change their current target to prevent mishaps

    //fov stuff
    public LayerMask friendlies, hostiles, obstacles;

    [Header("Field of Vision")]
    public Transform visionPoint;
    public float fovRadius, fovAngle;
    public List<GameObject> targets;

    public GameObject CurrentTarget;

    //~~Attack~~
    [Range(0f, 10f)]
    public float attackCoolDown;
    private float atkCD;
    [Range(0f, 10f)]
    public int attackDamage;
    public bool canAtk;
    [Range(0f, 10f)]
    public float aoeRadius; //default is 0

    //events
    public GameObject deathParticle;

    //references
    public List<SkinnedMeshRenderer> Meshes; //throw into the inspector the parts w materials
    protected HealthBarUI healthBarUI;
    FlagController fc;
    private void Awake()
    {
        fc = FindObjectOfType<FlagController>();
        healthBarUI = FindObjectOfType<HealthBarUI>();
        agent = GetComponent<NavMeshAgent>();
        canAtk = false;
        anim = GetComponent<Animator>();
        atkCD = 0f;
    }
    //method to check/update hp
    public void UpdateHealth(int dmgvalue)
    {
        //Subtract damage from health
        //if damage is positive, color entity red
        //else damage is negative (meaning healing), color entity green
        hp -= dmgvalue;
        if (Mathf.Sign(dmgvalue) == 1) UpdateColor(Color.red); //dmg color
        else if (Mathf.Sign(dmgvalue) == -1) UpdateColor(Color.green); //heal color

        //for players to update their health bar
        PlayerController pc = this.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            healthBarUI.UpdateHP(hp, pc);
        }
        //if entity is to die
        if (hp <= 0)
        {
            DeathEvent();
        }
        else StartCoroutine(ResetColor());
    }

    //update color
    void UpdateColor(Color color)
    {
        foreach (SkinnedMeshRenderer skm in Meshes)
        {
            skm.material.color = color;
        }
    }
    //reset color after a while
    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(.5f);
        UpdateColor(Color.white);
    }
    //death event
    public void DeathEvent()
    {
        //death animations
        anim.SetBool("IsDying", true);
        //remove the gameobject if its in the flagORplayer area (will auto check in the function)
        fc.UpdateEntity(this.gameObject, "Remove");
        Destroy(this.gameObject);
    }

    //Field of view function
    public void FieldOfVisionCheck(LayerMask targetLayer)
    {
        //If target is very close to entity, then js focus on it and dont bother about anything else
        //so this gives players chance to kill many enemies at the flagORplayer
        //or to prevent enemies from being stuck from changing to and fro from flagORplayer to friendlies
        if (CurrentTarget != null) {
            if (Vector3.Distance(this.transform.position, CurrentTarget.transform.position) <= lockOnDistance) return;
        }
        //reset list of targets
        targets.Clear();

        //Field of vision
        Collider[] colliders = Physics.OverlapSphere(visionPoint.position, fovRadius, targetLayer);

        //Go through all friendlies in the fov range
        foreach (Collider collider in colliders)
        {
            Vector3 distanceToGameObject = collider.gameObject.transform.position - visionPoint.position;

            //check if its within fov angle
            if (Vector3.Angle(visionPoint.forward, distanceToGameObject) <= fovAngle * 0.5f)
            {
                //check if got vision and not being blocked by obstacles
                bool canSee = Physics.Raycast(visionPoint.position, (collider.gameObject.transform.position - this.transform.position).normalized, Mathf.Infinity, ~obstacles);
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
            float newdistance = Vector3.Distance(go.transform.position, visionPoint.transform.position);
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
            CurrentTarget = closestFriendly;
        }
        else
        {
            //if friendly chase the closest player
            if (this.gameObject.GetComponent<FriendlyController>() != null)
            {
                //Find closest player
                PlayerController[] pcS = FindObjectsOfType<PlayerController>();
                
                //Check distance
                if ((Vector3.Distance(pcS[0].gameObject.transform.position, this.transform.position) < (Vector3.Distance(pcS[1].gameObject.transform.position, this.transform.position))))
                {
                    flagORplayer = pcS[0].gameObject.transform;
                }
                else flagORplayer = pcS[1].gameObject.transform;
            }
            agent.SetDestination(flagORplayer.position);
            CurrentTarget = flagORplayer.gameObject;
             

        }
    }

    //Attack

    //update cooldown
    public void UpdateAtkCD()
    {
        if (!canAtk && atkCD >= attackCoolDown)
        {
            canAtk = true;
            atkCD = 0;
        }
        else if (!canAtk && atkCD < attackCoolDown)
        {
            atkCD += Time.deltaTime;
        }
    }

    //attack move (both single-target and aoe)
    public void Attack(Collider collider, LayerMask targetLayer)
    {
        
        //if cooldown isnt up, return
        if (!canAtk) return;

        atkCD = 0;
        canAtk = false;

        //original target that is in collision
        GameObject target = collider.gameObject;

        //check if aoe or not
        if (aoeRadius == 0)
        {
            //play attack animation
            //

            //deal dmg to the target
            Entity entity = target.GetComponent<Entity>();
            if (entity != null)
            {
                entity.UpdateHealth(attackDamage);
            }
        }
        else
        {
            //Get a list of all colliders within a circle with the target as the center
            Collider[] colliders;

            colliders = Physics.OverlapSphere(target.gameObject.transform.position, aoeRadius, targetLayer);

            foreach (Collider c in colliders)
            {
                Entity entity = c.gameObject.GetComponent<Entity>();

                if (entity != null)
                {
                    entity.UpdateHealth(attackDamage);
                }
            }
        }
    }
}


