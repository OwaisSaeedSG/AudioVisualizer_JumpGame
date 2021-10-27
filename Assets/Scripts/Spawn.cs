using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject obstacles;
    private Vector3 spawnPos = new Vector3(10, (float)0.75, 0);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(obstacles, spawnPos, obstacles.transform.rotation);
        }
    }
}
