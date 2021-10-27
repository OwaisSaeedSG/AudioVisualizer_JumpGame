using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TandM : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.tag != "Collectable")
            gameObject.GetComponentInParent<AIMove2>().TopTrigger(c);
    }
}
