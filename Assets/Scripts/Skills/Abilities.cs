using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a class to be inherited from, which includes basic attack and skill 
//for each animal
public abstract class Abilities : MonoBehaviour
{
    //Cooldown
    [Header("Basic Attack")]
    [Range(0, 10f)]
    public float BasicCooldown; //maxcd
    protected float basiccd;
    [SerializeField]
    protected bool canBasic;

    [Header("Skill")]
    [Range(0f, 10f)]
    public float SkillCooldown;
    protected float skillcd;
    [SerializeField]
    protected bool canSkill;

    [Header("References")]
    public Transform firepoint;

    protected void Start()
    {
        //Set initial values
        basiccd = 0f;
        skillcd = 0f;
        canBasic = true;
        canSkill = true;
    }

    //Update CD whenever start skill or attack
    protected void Update()
    {
        //Basic atk cooldown update
        if (!canBasic)
        {
            if (basiccd >= BasicCooldown)
            {
                basiccd = 0;
                canBasic = true;
            }
            else basiccd += Time.deltaTime;
        }
        //Skill cooldown update
        if (!canSkill)
        {
            if (skillcd >= SkillCooldown)
            {
                skillcd = 0;
                canSkill = true;
            }
            else skillcd += Time.deltaTime; 
        }
    }
    //Basic Attack
    public virtual void BasicAttack()
    {

    }
    
    //Skill 1
    public virtual void Skill1()
    {

    }
}
