using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeToSelfDestruct = 2f;

    void Start()
    {
        Destroy(gameObject, timeToSelfDestruct);   
    }
}
