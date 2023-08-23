using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            GameObject player = other.gameObject;
          
            player.transform.position = new Vector3(destination.position.x
                , destination.position.y,
                 destination.position.z) + Vector3.forward;
        }
    }

}
