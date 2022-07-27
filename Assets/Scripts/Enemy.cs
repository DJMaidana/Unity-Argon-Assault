using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int scoreOnHit = 1;
    [SerializeField] int scoreOnDestroy = 1;
    [SerializeField] int hitPoints = 1;

    [SerializeField] GameObject vfx_EnemyExplosion;
    [SerializeField] GameObject vfx_EnemyHit;
    [SerializeField] GameObject parentGameObject;

    ScoreBoard scoreBoard;

    void Start()
    {
        gameObject.AddComponent<Rigidbody>();               //  Add a Rigidbody to the GameObject
        GetComponent<Rigidbody>().useGravity = false;       //  Disable gravity so it doesn't fall
        scoreBoard = FindObjectOfType<ScoreBoard>();        //  Get reference to scoreboard GameObject

        parentGameObject = GameObject.FindWithTag("Spawn Collector");       //  Get the transform of tag Spawn Collector
    }

    void OnParticleCollision(GameObject other)
    {
        if (hitPoints > 0)
        {
            EnemyHit();
        }
        else
        {
            EnemyDestroy();
        }
    }

    void EnemyHit()
    {
        GameObject vfx = Instantiate(vfx_EnemyHit, transform.position, transform.rotation);
        vfx.transform.parent = parentGameObject.transform;
        scoreBoard.IncreaseScore(scoreOnHit);
        hitPoints -= 1;
    }
    
    void EnemyDestroy()
    {
        scoreBoard.IncreaseScore(scoreOnDestroy);
        GameObject vfx = Instantiate(vfx_EnemyExplosion, transform.position, transform.rotation);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
