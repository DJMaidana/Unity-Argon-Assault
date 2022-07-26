using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    [SerializeField] GameObject playerRig;
    PlayerController playerController;
    [SerializeField] ParticleSystem vfx_Explosion;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " triggered by " + other.gameObject.name);
        CrashSequence();
    }

    void CrashSequence()
    {
        playerController.enabled = false;
        playerRig.GetComponent<Animator>().enabled = false;
        vfx_Explosion.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
