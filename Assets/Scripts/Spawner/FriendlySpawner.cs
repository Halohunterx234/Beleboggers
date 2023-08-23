using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlySpawner : MonoBehaviour
{
    //Spawn all friendly entities

    //Players
    public GameObject player1, player2;

    //Friendleis
    public GameObject bee, elephant;

    //Radius
    public float radius =10f;

    //Respawn method
    public IEnumerator IERespawn(GameObject entity)
    {
        //Delay for players cuz need play death events
        if (entity.GetComponent<PlayerController>() != null) entity.SetActive(false);
        print("rereg");
        //yield return new WaitForSeconds(delay);
        print("respawning");
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
