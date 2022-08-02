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

    PlayerController playerController;
    AudioSource audioSource;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " triggered by " + other.gameObject.name);
        CrashSequence();
    }

    void CrashSequence()
    {
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
