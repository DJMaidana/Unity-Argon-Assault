using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int scoreAwarded;

    [SerializeField] GameObject vfx_EnemyExplosion;
    [SerializeField] Transform parent;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        scoreBoard.IncreaseScore(scoreAwarded);
        Instantiate(vfx_EnemyExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
