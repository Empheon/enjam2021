using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Pigeon : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Train"))
            {
                Debug.Log("ded");
            }
        }
    }
}