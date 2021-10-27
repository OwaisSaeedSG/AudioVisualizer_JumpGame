using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStuffLeft : MonoBehaviour
{

    public float speed;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
