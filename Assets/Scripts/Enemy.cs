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
    [SerializeField] Transform parent;

    ScoreBoard scoreBoard;

    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (hitPoints > 0)
        {
            GameObject vfx = Instantiate(vfx_EnemyHit, transform.position, transform.rotation);
            vfx.transform.parent = parent;
            scoreBoard.IncreaseScore(scoreOnHit);
            hitPoints -= 1;
        }
        else
        {
            scoreBoard.IncreaseScore(scoreOnDestroy);            
            GameObject vfx = Instantiate(vfx_EnemyExplosion, transform.position, transform.rotation);
            vfx.transform.parent = parent;
            Destroy(gameObject);
        }
    }
}
