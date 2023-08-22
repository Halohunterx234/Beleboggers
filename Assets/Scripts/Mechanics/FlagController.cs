using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    //To be attached to the cylinder with a capsule trigger collider at the base of the flag
    //this is the capturing area of the flag that entities need to stay within

    //In charge of the following:
    //1. detecting when entities are at the flag
    //2. starting a 6 second period when only entities of one team are touching thw flag
    //3. resetting the period when another team joins, or the previous team left
    //4. every 2 seconds, send the number of entities to the scoremanager to 
    //calculate and update the current score

    [Header("Capturing Area")]
    //6 Second Period (Capturing Area)
    bool capturingArea;
    float capturingAreaCD;
    [Range(0f, 10f)]
    public float capturingAreaPeriod;

    [Header("Capturing Flag")]
    //2 Second Period (Capturing Flag..) 
    bool capturingFlag;
    float capturingFlagCD;
    [Range(0f, 10f)]
    public float capturingFlagPeriod;

    //Tracking of entities within flag area
    public List<GameObject> Entities;
    private int Friendly, Hostile;
    private bool friendlyTeamLast, hostileTeamLast;

    //Internal References
    CapsuleCollider capsule;

    //External References
    ScoreController sc;

    private void Start()
    {
        //Set initial values
        capturingFlagCD = 0;
        capturingAreaCD = 0;
        capturingArea = false;
        capturingFlag = false;
        friendlyTeamLast = false;
        hostileTeamLast = false;
        sc = FindObjectOfType<ScoreController>();
        capsule = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        //if entities of only one team are in the flag capturing area
        if (capturingArea)
        {
            //if six seconds are over, start capturing the flag
            if (capturingAreaCD >= capturingAreaPeriod)
            {
                //Reset cooldown and start the capturingflag loop
                capturingAreaCD = 0;
                capturingFlag = true;
                capturingArea = false;
            }
            else capturingAreaCD += Time.deltaTime;
        }
        //if entities of only one team are capturing the flag
        else if (capturingFlag)
        {
            if (capturingFlagCD >= capturingFlagPeriod)
            {
                //Reset cooldown and update score using scoremanager
                capturingFlagCD = 0;
                sc.AddScore(Entities.Count, Entities[0].GetComponent<Entity>());
            }
        }
    }

    //method to check the current team composition on the flag area
    private void CheckArea()
    {
        //Check for the below situations

        //0. if the flag has no prior entities on it, and a new team gets on it
        //1. if a team is already capturing, and another entity of the other team steps in
        //this will reset the capturing flag and set it to false
        //2. or Situation 1 happened, then that foreign entity has died. So the surviving team
        //does not need to wait 6 seconds again, and their capturingflag starts immediately
        //3. or situation 1 happened, but the other result happened. So the new team has to wait 
        //a new 6 seconds to re capture the area before starting capturing flag
        //4. no entities are on the flag, which resets all capturingflag and capturingareas

        //Disrupt any capturing
        if (Hostile > 0 && Friendly > 0)
        {
            if (capturingFlag || capturingArea)
            {
                capturingFlag = false;
                capturingArea = false;
            }
        }

        //first time capture

        //if hostile team wins
        else if ((Hostile > 0 && Friendly == 0))
        {
            capturingFlag = false;
            capturingArea = true;

            //if the hostile team was capturing prior
            if (hostileTeamLast && !friendlyTeamLast)
            {
                //no need to reset cd
                //it should be frozen and will continue
            }
            //if the friendly team was capturing prior
            else if (friendlyTeamLast && !hostileTeamLast)
            {
                friendlyTeamLast = false;
                hostileTeamLast = true;
                //Reset cd
                capturingAreaCD = 0;
            }
        }

        //if friendly team took over hostile team
        else if ((Friendly > 0 && Hostile == 0) && hostileTeamLast)
        {
            capturingFlag = false;
            capturingArea = true;

            //if the friendly team was capturing prior
            if (!hostileTeamLast && friendlyTeamLast)
            {
                //no need to reset cd
                //it should be frozen and will continue
            }
            //if the hostile team was capturing prior
            else if (!friendlyTeamLast && hostileTeamLast)
            {
                friendlyTeamLast = true;
                hostileTeamLast = false;
                //Reset cd
                capturingAreaCD = 0;
            }
        }

    }

    //This checks every entity that entered the flag area for the first time
    private void OnTriggerEnter(Collider other)
    {
        //Check if it is a entity
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            //check in case it is somehow already in the list
            if (Entities.Contains(other.gameObject)) return;

            //Fid out whether its a friendly or enemy
            if (entity.GetType() == typeof(PlayerController))
            {
                //its a player
                Friendly++;
            }
            else if (entity.GetType() == typeof(EnemyController))
            {
                //its a enemy
                Hostile++;
            }

            //update fleet composition and check whether any changes should be made accordingly
            CheckArea();
        }
    }

    //
    private void OnTriggerStay(Collider other)
    {

    }

    //Make a check if there are any changes to the current team war
    private void OnTriggerExit(Collider other)
    {
        //Check if it is a entity
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            //check in case it is somehow already in the list
            if (Entities.Contains(other.gameObject)) return;

            //Fid out whether its a friendly or enemy
            if (entity.GetType() == typeof(PlayerController))
            {
                //its a player
                Friendly--;
            }
            else if (entity.GetType() == typeof(EnemyController))
            {
                //its a enemy
                Hostile--;
            }

            //check area
            CheckArea();
        }
    }
}
