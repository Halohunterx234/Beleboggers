using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    //base class for all projectiles
    [Header("Stats"), Range(0f, 10f)]
    public float despawnTime;
    public float speed; public int damage;
    public GameObject chicken;
    public GameController gc;
    protected virtual void Awake()
    {
        //Despawn in x number of seconds
        Destroy(this.gameObject, despawnTime);
        gc = FindObjectOfType<GameController>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //check if collision is a enemy
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            //if the projectile hits a enemy
            Entity e = collision.gameObject.GetComponent<Entity>();

            //double dmg
            if (gc.dealsdoubledmg)
            {
               e.UpdateHealth(2*damage);
            }
            else e.UpdateHealth(damage);
            Destroy(this.gameObject);
        }
    }
}
