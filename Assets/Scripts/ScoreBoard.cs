using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    float score;

    public void IncreaseScore(int scoreAwarded)
    {
        score += scoreAwarded;
        Debug.Log("Score: " + score);
    }
}
