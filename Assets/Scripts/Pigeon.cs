using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Pigeon : MonoBehaviour
    {
        public static int DeathCount = 0;
        public ParticleSystem Poof;
        public GameObject PigeonMesh;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Train"))
            {
                DeathCount++;
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.Play();

                Destroy(PigeonMesh);
                Poof.Play();
                // PigeonMesh.transform.DOScale(3, 0.5f).SetEase(Ease.InCubic).OnComplete(
                //     () =>
                //     {
                //         Destroy(PigeonMesh);
                //         Poof.Play();
                //     });
            }
        }
    }
}