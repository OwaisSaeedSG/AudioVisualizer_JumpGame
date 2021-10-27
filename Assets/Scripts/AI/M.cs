using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.tag != "Collectable")
            gameObject.GetComponentInParent<AIMove2>().MidTrigger(c);
    }
}
