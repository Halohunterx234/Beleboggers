using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Egglosion : Projectiles
{
    [Range(0f, 50f)]
    public float egglosionRadius;
    

    protected override void Awake()
    {
        Destroy(this.gameObject, despawnTime);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Eggplode());
    }

    IEnumerator Eggplode(float t=0)
    {
        //Delay if needed
        yield return new WaitForSeconds(t);

        //Get a list of all colliders in the egglosion range
        Collider[] colliderslist = Physics.OverlapSphere(transform.position, egglosionRadius);

        //go through each and check if tis a enemy
        foreach (Collider collider in colliderslist)
        {
            if (collider.gameObject.GetComponent<EnemyController>() != null)
            {
                //do damage
                Entity entity = collider.gameObject.GetComponent<EnemyController>();
                entity.UpdateHealth(damage);
            }
        }

        Destroy(this.gameObject);
    }
}
