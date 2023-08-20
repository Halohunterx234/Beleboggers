using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAbility : Abilities
{
    [Header("Projectile")]
    public GameObject fishGO;
    private float ogCD;

    [Header("Stats"), Range(0f, 10f)]
    public float fishSpeed;
    public int fishDamage;

    private void Awake()
    {
        ogCD = BasicCooldown;
    }
    //Shoot fishes
    public override void BasicAttack()
    {
        if (!canBasic) return;
        canBasic = false;
        GameObject fish = Instantiate(fishGO, transform.position + transform.forward, transform.rotation);
        fish.GetComponent<Rigidbody>().velocity = transform.forward * fishSpeed;
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
