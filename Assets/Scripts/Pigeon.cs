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
        public GameObject BloodSplatter;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            BloodSplatter.SetActive(false);

            PigeonMesh.transform.DOPunchScale(Vector3.one * 0.05f, Random.Range(1.5f, 2f), 1).SetLoops(-1)
                .SetDelay(Random.Range(0, 1));
            
            transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Train"))
            {
                DeathCount++;
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.Play();

                PigeonMesh.transform.DOKill();
                Destroy(PigeonMesh);
                BloodSplatter.SetActive(true);
                Vector3 sc = BloodSplatter.transform.localScale;
                BloodSplatter.transform.DOScale(sc, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
                Poof.Play();
                
                CameraShake.Instance.Shake(0.1f, 0.1f);
                
                // PigeonMesh.transform.DOScale(3, 0.5f).SetEase(Ease.InCubic).OnComplete(
                //     () =>
                //     {
                //         Destroy(PigeonMesh);
                //         Poof.Play();
                //     });
            }
        }

        private void OnDestroy()
        {
            if (PigeonMesh)
            {
                PigeonMesh.transform.DOKill();
            }
        }
    }
}