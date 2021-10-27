using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    public Color color1;
    public Color color2;
    public float duration = 3.0F;

    public Camera cam;

    void Start()
    {

    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp(color1, color2, t);

        if (Input.GetKeyDown("s"))
        {
            color2 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    void OnMouseDown()
    {
        // Pick a random, saturated and not-too-dark color
        cam.backgroundColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
