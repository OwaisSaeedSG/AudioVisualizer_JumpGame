using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public GameObject level;
    public Transform player;
    private Vector3 spawnPos;
    bool end = false;
    bool stop = true;

    private void Start()
    {
        spawnPos = new Vector3(15, 0, 0);
        stop = true;
    }

    void Update()
    {
        if (end && stop)
        {
            Instantiate(level, spawnPos, level.transform.rotation);
            
            end = false;
            stop = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        end = true;
    }

    void OnBecameInvisible()
    {
        if(gameObject.name != "Floor (8)")
        Destroy(level);
    }
}
