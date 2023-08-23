using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySpawner : MonoBehaviour
{
    //Spawn all friendly entities


    //flag reference
    
    //Players
    public GameObject player1, player2;

    //Friendleis
    public GameObject bee, elephant;
    public float rng, rngelephant = 3;
    //Radius
    public float radius =10f;

    //spawns friendlies every now and then
    public float spawnCD, spawnCDMax, spawnCDRangeMin, spawnCDRangeMax;

    private void Awake()
    {
        spawnCD = 0;
        spawnCDRangeMin = 15;
        spawnCDRangeMax = 12.5f;
        spawnCDMax = Random.Range(spawnCDRangeMin, spawnCDRangeMax);
    }

    private void Update()
    {
        if (spawnCD >= spawnCDMax)
        {
            spawnCD = 0;
            spawnCDMax = Random.Range(spawnCDRangeMin, spawnCDRangeMax);
            Spawn();
        }
        else spawnCD += Time.deltaTime;
    }

    //spawn method

    public void Spawn()
    {
        //decide if to spawn rng
        rng = Random.Range(0, 5);

        //get random position
        //in a radius first
        Vector2 radiusPosition = Random.insideUnitCircle * radius;
        Vector3 spawnPosition = new Vector3(radiusPosition.x + transform.position.x, 0, radiusPosition.y + transform.position.z);

        //spawn
        if (rng == rngelephant)
        {
            //yeti
            GameObject enemy = Instantiate(elephant, spawnPosition, Quaternion.identity);
        }
        else
        {
            //hantu
            GameObject enemy = Instantiate(bee, spawnPosition, Quaternion.identity);
        }
        
    }
    //Respawn method
    public IEnumerator IERespawn(GameObject entity)
    {
        //Delay for players cuz need play death events
        if (entity.GetComponent<PlayerController>() != null) entity.SetActive(false);
        
        //for players, reset them and all their stats
        if (entity.activeSelf == false)
        {
            entity.SetActive(true);
            PlayerController pc = entity.GetComponent<PlayerController>();
            pc.hp = 100;
            entity.GetComponent<Entity>().UpdateHealth(0);
            Animator anim = entity.GetComponent<Animator>();
            anim.SetBool("IsDying", false);
        }
        CharacterController cc = entity.GetComponent<CharacterController>();

        //get a random point on the respawn circle
        Vector3 RespawnPosition = transform.position + (Random.onUnitSphere * radius) + Vector3.up*5f;

        //Disable character controller for players to respawn cuz they need teleport
        if (cc != null)
        {
            cc.enabled = false;
        }

        //tp
        entity.transform.position = RespawnPosition;

        if (cc != null)
        {
            cc.enabled = true;
        }
        yield return null;
    }
}
