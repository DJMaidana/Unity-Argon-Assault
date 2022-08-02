using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] AudioClip sfxLaser;

    AudioSource audioSource;

    int numberOfParticles = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        int particleCount = GetComponent<ParticleSystem>().particleCount;

        if (particleCount > numberOfParticles)
        {
            audioSource.PlayOneShot(sfxLaser);
        }

        numberOfParticles = particleCount;
    }
}
