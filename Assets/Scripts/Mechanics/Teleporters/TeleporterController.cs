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
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            pc.Tp(destination.transform.position + 3*Vector3.forward);
        }
    }

}
