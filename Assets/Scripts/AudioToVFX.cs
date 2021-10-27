using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class AudioToVFX : MonoBehaviour
{
    public int multiplyWithAmp;
    public int multiplyWithRadius;
    public int multiplyWithFrequencyLow;
    public int multiplyWithFrequencyHigh;

    public AudioData_AmplitudeBand ampFromAudio;

    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private float VFX_intensity;
    [SerializeField] private float VFX_radius;
    [SerializeField] private Vector3 VFX_gravity;
    [SerializeField] private float baseBeatValue;

    // Color Changes withn beats, needs more work
    [ColorUsage(true, true)]
    [SerializeField] private Color[] VFX_Color;
    [ColorUsage(true, true)]
    [SerializeField] private Color[] fraquencyColors;

    //get and keep Frequency bands
    [SerializeField] private float[] frequencyList;

    [SerializeField] private float particleStartDepth;
    // Start is called before the first frame update
    void Start()
    {
        particleStartDepth = transform.position.z;
        frequencyList = AudioData_AmplitudeBand._freqBand;
    }

    // Update is called once per frame
    void Update()
    {
        // High intenisty (aka sound) spawns more particles 
        float a = ampFromAudio.Amplitude;
        VFX_intensity = a * multiplyWithAmp;

        // This increases the radius when sound is louder
        float b = ampFromAudio.Amplitude;
        VFX_radius = b * multiplyWithRadius;

        // Get frequency band list for gravity
        float c = frequencyList[0] * multiplyWithFrequencyLow;
        float d = frequencyList[4] * multiplyWithFrequencyHigh;
        float e = c + d;

        VFX_gravity = new Vector3(0, -e, 0);

        // Set the values we got
        visualEffect.SetFloat("VFX_Intensity", VFX_intensity);
        visualEffect.SetFloat("VFX_Radius", VFX_radius);
        visualEffect.SetVector3("VFX_Gravity", VFX_gravity);


        if (frequencyList[4] < 0.01f)
        {
            VFX_Color[0] = fraquencyColors[0];
            BeatOcurred();
        }
        else if (frequencyList[4] < 0.3f)
        {
            VFX_Color[0] = fraquencyColors[1];
            BeatOcurred();
        }
        else if (frequencyList[4] < 1)
        {
            VFX_Color[0] = fraquencyColors[2];
            BeatOcurred();

        }
        else if (frequencyList[4] > 1)
        {
            VFX_Color[0] = fraquencyColors[3];
            BeatOcurred();
        }

        /*
        if (e > 0)
        {
            VFX_Color[0] = fraquencyColors[0];
        }
        else
        {
            VFX_Color[0] = fraquencyColors[1];
        }
        */

        visualEffect.SetVector4("Seed_Color", new Vector3(VFX_Color[0].r, VFX_Color[0].g, VFX_Color[0].b));

        Debug.Log(visualEffect.aliveParticleCount);



    }

    bool isMovementAllowed = true;
    public void BeatOcurred()
    {
        if (isMovementAllowed)
        {
            transform.DOMoveZ(Mathf.Clamp((particleStartDepth-(baseBeatValue * frequencyList[4])), 0, 6), 0.1f, false).OnComplete(() =>
              {
                  isMovementAllowed = false;
                  transform.DOMoveZ(particleStartDepth, Mathf.Clamp((1 * baseBeatValue * frequencyList[4]), 0f, 1f), false).OnComplete(() =>
                  {
                      isMovementAllowed = true;
                  });
              });
        }

    }
}
