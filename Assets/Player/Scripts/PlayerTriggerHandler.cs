using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    private Collider collider;

    private void Start()
    {
        collider = gameObject.GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Kiosk")
        {
            // PlayerController interaction
        }
    }
}
