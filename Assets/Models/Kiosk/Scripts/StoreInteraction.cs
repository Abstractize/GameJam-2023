using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiosk
{
    public class StoreInteraction : MonoBehaviour
    {
        [SerializeField] private string _name = "Store";

        private void OnTriggerEnter(Collider other)
            => Debug.Log($"{other.name} collided");
    }
}

