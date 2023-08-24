using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    //Visual references
    public Material[] materials;

    // 0 - Damage buff
    // 1 - Health boost
    // 2 - Spawn Bee
    // 3 - Spawn elephant

    //rng
    [SerializeField]
    private int collectibleNum;

    //Internal referencs
    MeshRenderer mr;

    //External references
    public GameObject bee, elephant;
    PlayerController[] playerController;
    CollectiblesSpawner cs;
    GameController gc;

    //Skill Stats
    [Range(0f, 10f)]
    public float DamageBuff;
    [Range(0, 100)]
    public int Healing;

    //Class for all collectibles, will randomise upon spawn

    [SerializeField]
    private bool isSpawned;

    //
    private void Awake()
    {
        Spawn();
        isSpawned = true;
        playerController = FindObjectsOfType<PlayerController>();
        cs = GetComponentInParent<CollectiblesSpawner>();
        gc = FindObjectOfType<GameController>();
    }

    //Spawning
    void Spawn()
    {
        mr = GetComponent<MeshRenderer>();
        //Set visuals
        collectibleNum = Random.Range(0, 3);
        mr.material = materials[collectibleNum];
    }

    //Select
    void Ability()
    {

        if (collectibleNum == 0)
        {
            Damage();
        }
        else if (collectibleNum == 1)
        {
            Heal();
        }
        else if (collectibleNum == 2)
        {
            SpawnBee();
        }
        else if (collectibleNum == 3)
        {
            SpawnElephant();
        }
    }

    //Abiltiies
    void Damage()
    {
        StartCoroutine(gc.DmgBuff());
        foreach (PlayerController pc in playerController)
        {
            StartCoroutine(pc.dmgboost());
        }
    }

   
    void Heal()
    {
        foreach (PlayerController pc in playerController)
        {
            Entity entity = pc.gameObject.GetComponent<Entity>();
            entity.UpdateHealth(-50);
        }
    }

    void SpawnBee()
    {
        Instantiate(bee, this.transform.position, Quaternion.identity);
    }

    void SpawnElephant()
    {
        Instantiate(elephant, this.transform.position, Quaternion.identity);
    }
    //Collision code
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            Ability();
            Destroy(this.gameObject);
        }
        
    }
}
