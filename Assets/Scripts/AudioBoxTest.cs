using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBoxTest : MonoBehaviour
{

    //get and keep Frequency bands
    public AudioData_AmplitudeBand ampFromAudio;

    public int band;
    public float startScale, maxScale;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(transform.localScale.x, (AudioData_AmplitudeBand._audioBandBuffer[band] * maxScale) + startScale, transform.localScale.z);
        
        transform.localScale = new Vector3(transform.localScale.x, (ampFromAudio.Amplitude * maxScale) + startScale, transform.localScale.z);
    }

    
}
