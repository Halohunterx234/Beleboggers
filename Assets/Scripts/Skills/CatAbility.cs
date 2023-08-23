using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatAbility : Abilities
{
    [Header("Projectile")]
    public GameObject fishGO;
    private float ogCD;

    [Header("Stats"), Range(0f, 10f)]
    public float fishSpeed;
    public int fishDamage;

    [Header("Materials")]
    public Material normalFish, fastFish;

    [Header("UI")]
    public Image BasicAttackImg, SkillImg;

    private void Awake()
    {
        //Set temp cooldown variable value
        ogCD = BasicCooldown;
        fishGO.GetComponent<MeshRenderer>().material = normalFish;
    }
    //Shoot fishes
    public override void BasicAttack()
    {
        if (!canBasic) return;
        canBasic = false;


        //spawn fish, rotate it
        //and yoink it
        GameObject fish = Instantiate(fishGO, firepoint.position + firepoint.forward, firepoint.rotation);
        fish.transform.Rotate(0, 90, 90);
        fish.GetComponent<Rigidbody>().velocity = transform.forward * fishSpeed;

        //update its projectiles data
        Projectiles p = fish.GetComponent<Projectiles>();
        p.damage = fishDamage; p.speed = fishSpeed;
    }

    //Skill -> Enhancing attack speed
    public override void Skill1()
    {
        if (!canSkill) return;
        canSkill = false;
        
        //Lower cd -> higher firing rate
        BasicCooldown *= 0.5f;
        StartCoroutine(ResetCD());

        //change of material purely to differentiate in skill state and not
        fishGO.GetComponent<MeshRenderer>().material = fastFish;
    }

    //Timer to reset the buffed cd to its original state
    IEnumerator ResetCD()
    {
        yield return new WaitForSeconds(3);
        //Reset cd
        BasicCooldown = ogCD;
        fishGO.GetComponent<MeshRenderer>().material = normalFish;
    }
}
