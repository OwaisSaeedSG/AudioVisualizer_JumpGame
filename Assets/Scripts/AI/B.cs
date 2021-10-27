using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{
    void OnTriggerExit(Collider c)
    {
        if(c.tag != "Collectable")
            gameObject.GetComponentInParent<AIMove2>().BottomTrigger(c);
    }
}
