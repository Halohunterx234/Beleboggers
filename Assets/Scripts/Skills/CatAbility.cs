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

    //audio
    public AudioSource atkSource;

    //external
    GameController gc;

    private void Awake()
    {
        //Set temp cooldown variable value
        ogCD = BasicCooldown;
        fishGO.GetComponent<MeshRenderer>().material = normalFish;
        isBuffed = false;
        gc = FindObjectOfType<GameController>();
    }
    //Shoot fishes
    public override void BasicAttack()
    {
        if (!canBasic || isLocked) return;
        canBasic = false;


        //spawn fish, rotate it
        //and yoink it
        GameObject fish = Instantiate(fishGO, firepoint.position + firepoint.forward, firepoint.rotation);
        fish.transform.Rotate(-80, 144, 130);
        fish.GetComponent<Rigidbody>().velocity = transform.forward * fishSpeed;

        //if in skill state, shoot two mroe fishes in two directions
        if (isBuffed )
        {
            atkSource.Play();
            GameObject fish1 = Instantiate(fishGO, firepoint.position + firepoint.forward + 0.5f * Vector3.left, firepoint.rotation);
            fish1.transform.Rotate(-80, 44, 220);
            fish1.GetComponent<Rigidbody>().velocity = transform.forward * fishSpeed + Vector3.left * 0.5f;
            GameObject fish2 = Instantiate(fishGO, firepoint.position + firepoint.forward + 0.5f*Vector3.right, firepoint.rotation);
            fish2.transform.Rotate(-80, 54, 220);
            fish2.GetComponent<Rigidbody>().velocity = transform.forward * fishSpeed + Vector3.right * 0.5f;
        }
        
        //update its projectiles data
        Projectiles p = fish.GetComponent<Projectiles>();
        if (gc.dealsdoubledmg) p.damage = 2 * fishDamage;
        else p.damage = fishDamage;
        p.speed = fishSpeed;
    }

    //Skill -> Enhancing attack speed
    public override void Skill1()
    {
        if (!canSkill || isLocked) return;
        canSkill = false;
        
        //Lower cd -> higher firing rate
        BasicCooldown *= 0.5f;
        isBuffed = true;
        cooldownbarUI.BuffShade();

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
        isBuffed = false;
        fishGO.GetComponent<MeshRenderer>().material = normalFish;
    }
}
