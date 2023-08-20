using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAbility : Abilities
{
    [Header("Projectile")]
    public GameObject eggGO;
    private float ogCD;

    [Header("Stats"), Range(0f, 10f)]
    public float eggSpeed;
    public int eggDamage;

    //Pivot
    private Transform firepoint;

    private void Awake()
    {
        ogCD = BasicCooldown;
        firepoint = GetComponentsInChildren<Transform>()[10].gameObject.transform; 
    }
    //Shoot eggs
    public override void BasicAttack()
    {
        if (!canBasic) return;
        canBasic = false;
        GameObject egg = Instantiate(eggGO, firepoint.position, transform.rotation);
        egg.transform.Rotate(0, 90, 90);
        egg.GetComponent<Rigidbody>().velocity = firepoint.forward * 2*eggSpeed + Vector3.up * 2*eggSpeed;
    }

    //Skill -> Enhancing attack speed
    public override void Skill1()
    {
        if (!canSkill) return;
        canSkill = false;
        //Lower cd -> higher firing rate
        BasicCooldown *= 0.5f;
        StartCoroutine(ResetCD());
    }

    //Timer to reset the buffed cd to its original state
    IEnumerator ResetCD()
    {
        yield return new WaitForSeconds(3);
        //Reset cd
        BasicCooldown = ogCD;
    }
}
