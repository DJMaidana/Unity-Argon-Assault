using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;

    [SerializeField] GameObject playerRig;
    [SerializeField] ParticleSystem vfx_Explosion;
    [SerializeField] AudioClip sfxExplosion;

    [SerializeField] GameObject vfxToDestroy;
    PlayerController playerController;
    AudioSource audioSource;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        CrashSequence();
    }

    void CrashSequence()
    {
        Destroy(vfxToDestroy);
        playerController.FiringLasers(false);
        playerController.enabled = false;
        playerRig.GetComponent<Animator>().enabled = false;
        vfx_Explosion.Play();
        audioSource.PlayOneShot(sfxExplosion);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
