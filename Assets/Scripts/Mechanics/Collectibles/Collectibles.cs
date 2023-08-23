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
        isSpawned = true;
        playerController = FindObjectsOfType<PlayerController>();
    }

    //Spawning
    void Spawn()
    {
        //Set visuals
        collectibleNum = Random.Range(0, materials.Length);
        mr.material = materials[collectibleNum];
    }

    //Select
    void Ability()
    {
        if (collectibleNum == 0)
        {

        }
        else if (collectibleNum == 1)
        {

        }
        else if (collectibleNum == 2)
        {

        }
        else if (collectibleNum == 3)
        {

        }
    }

    //Abiltiies
    void Damage()
    {
        foreach (PlayerController pc in playerController)
        {
            StartCoroutine(pc.dmgboost());
        }
    }

    void Heal()
    {

    }

    void SpawnBee()
    {

    }

    void SpawnElephant()
    {

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
