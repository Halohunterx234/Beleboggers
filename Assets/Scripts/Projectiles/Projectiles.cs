using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    //base class for all projectiles
    [Header("Stats"), Range(0f, 10f)]
    public float despawnTime;
    public float speed; public int damage;

    private void Awake()
    {
        Destroy(this.gameObject, despawnTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            //if the projectile hits a enemy
            Entity e = collision.gameObject.GetComponent<Entity>();
            e.UpdateHealth(damage);
            Destroy(this.gameObject);
        }
    }
}
