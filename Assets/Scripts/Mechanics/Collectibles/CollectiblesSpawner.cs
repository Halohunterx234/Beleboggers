using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollectiblesSpawner : MonoBehaviour
{
    //In charge of spawning new collectibles when picked up after 30 seconds
    public GameObject collectible;

    //cooldown
    public float cd, cdmax = 30;

    private void Start()
    {
        cd = 0;
        SpawnCollectible();
    }

    private void Update()
    {
        if (this.transform.childCount == 0)
        {
            if (cd >= cdmax)
            {
                cd = 0;
                SpawnCollectible();
            }
            else cd += Time.deltaTime;
        }
    }
    //spawn
    void SpawnCollectible()
    {
        GameObject go = Instantiate(collectible, transform);
        go.transform.position = this.transform.position;
        Collectibles c = go.GetComponent<Collectibles>();
    }


}
