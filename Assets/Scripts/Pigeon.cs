using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Pigeon : MonoBehaviour
    {
        public static int DeathCount = 0;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Train"))
            {
                Debug.Log("ded");
                DeathCount++;
            }
        }
    }
}