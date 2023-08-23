using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //Flag references
    public Transform flag;

    //Reponsible for all enemies spawning
    public float radius = 10f;

    //cooldown
    [SerializeField]
    private float cd, cdmax, cdrangemin, cdrangemax;

    //enemies
    public GameObject hantu, yeti;

    //chances
    private int rng, rngyeti = 3;

    private void Awake()
    {
        //start first set
        cd = 0;
        cdrangemin = 7.5f;
        cdrangemax = 10f;
        cdmax = Random.Range(cdrangemin, cdrangemax);
    }
    private void Update()
    {
        //run through cd to randomly spawn enemies
        if (cd >= cdmax)
        {
            cd = 0;
            Spawn();
        }
        else cd += Time.deltaTime;
    }

    //spawn method
    public void Spawn()
    {
        //decide if to spawn rng
        rng = Random.Range(0, 5);

        //get random position
        Vector2 radiusPosition = Random.insideUnitCircle * radius;
        Vector3 spawnPosition = new Vector3(radiusPosition.x + transform.position.x, 0, radiusPosition.y + transform.position.z);

        //spawn
        if (rng == rngyeti)
        {
            //yeti
            GameObject enemy = Instantiate(yeti, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().flagORplayer = flag;
        }
        else
        {
            //hantu
            GameObject enemy = Instantiate(hantu, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().flagORplayer = flag;
        }
        
    }
}
