using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAbility : Abilities
{
    [Header("Projectile")]
    public GameObject eggGO, eggBigGO;
    private float ogCD;

    [Header("Stats"), Range(0f, 20f)]
    public float eggSpeed, eggBigSpeed;
    public int eggDamage, eggBigDamage;

    //Pivot

    private void Awake()
    {

    }
    //Shoot eggs
    public override void BasicAttack()
    {
        if (!canBasic) return;

        //Reset cooldown and start update loop
        canBasic = false;
        
        //Spawn egg projectile
        GameObject egg = Instantiate(eggGO, firepoint.position + firepoint.forward, transform.rotation);
        egg.transform.Rotate(0, 90, 90);

        //Eggs are slow, have gravity, so will propel it in a arc 
        egg.GetComponent<Rigidbody>().velocity = firepoint.forward * eggSpeed + Vector3.up * eggSpeed;

        //update its projectiles data
        Projectiles p = egg.GetComponent<Projectiles>();
        p.damage = eggDamage; p.speed = eggSpeed;
    }

    //Skill -> Yoink a big aoe egg
    public override void Skill1()
    {
        if (!canSkill) return;
        canSkill = false;

        //shoot big egg
        GameObject bigegg = Instantiate(eggBigGO, firepoint.position + 2*firepoint.forward, transform.rotation);
        bigegg.transform.Rotate(0, 90, 90);

        //velocity, 
        bigegg.GetComponent<Rigidbody>().velocity = firepoint.forward * eggBigSpeed + Vector3.up * eggBigSpeed;

        //update projectile's damage and speed
        Projectiles p = bigegg.GetComponent<Projectiles>();
        p.damage = eggBigDamage; p.speed = eggBigSpeed;
    }


}
