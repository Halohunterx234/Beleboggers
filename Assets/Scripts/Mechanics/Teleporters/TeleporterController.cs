using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            print("teleporting");
            other.gameObject.transform.position = destination.transform.position + Vector3.forward;
        }
    }
}
