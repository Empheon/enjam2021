using System;
using PathCreation;
using UnityEngine;

namespace DefaultNamespace
{
    public class Track : MonoBehaviour
    {
        public PathCreator Path;
        [HideInInspector] public bool IsDestination;
    }
}