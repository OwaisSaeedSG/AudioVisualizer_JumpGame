using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AudioToVFX2 : MonoBehaviour
{
    private GameObject VFXgraph;
    public int multiplyWithAmp;
    public int multiplyWithRadius;
    public int multiplyWithFrequencyLow;
    public int multiplyWithFrequencyHigh;

    public AudioData_AmplitudeBand ampFromAudio;

    [SerializeField]
    private VisualEffect visualEffect;
    [SerializeField]
    private float VFX_intensity;
    [SerializeField]
    private float VFX_radius;
    [SerializeField]
    private Vector3 VFX_gravity;
    [SerializeField]
    private Color VFX_Color;
    [SerializeField] private GradientColorKey[] seedGradientColorKeys;
    [SerializeField] private GradientAlphaKey[] seedGradientAlphaKeys;
    [SerializeField] private Gradient seedGradient;
    [SerializeField] private Color[] fraquencyColors;

    //get and keep Frequency bands
    [SerializeField] private float[] frequencyList;

    // Start is called before the first frame update
    void Start()
    {
        VFXgraph = this.gameObject;
        frequencyList = AudioData_AmplitudeBand._freqBand;

        seedGradientAlphaKeys = new GradientAlphaKey[4];
        seedGradientAlphaKeys[0].alpha = 0f;
        seedGradientAlphaKeys[0].time = 0f;
        seedGradientAlphaKeys[1].alpha = 1f;
        seedGradientAlphaKeys[1].time = 0.1f;
        seedGradientAlphaKeys[2].alpha = 1f;
        seedGradientAlphaKeys[2].time = 0.9f;
        seedGradientAlphaKeys[3].alpha = 0f;
        seedGradientAlphaKeys[3].time = 1f;

        seedGradientColorKeys = new GradientColorKey[2];
        seedGradientColorKeys[0].color = Color.white;
        seedGradientColorKeys[0].time = 0f;
        seedGradientColorKeys[1].color = Color.white;
        seedGradientColorKeys[1].time = 1f;

        seedGradient.SetKeys(seedGradientColorKeys, seedGradientAlphaKeys);
    }

    // Update is called once per frame
    void Update()
    {
        // High intenisty (aka sound) spawns more particles 
        float a = ampFromAudio.Amplitude;
        VFX_intensity = a * multiplyWithAmp * 2;

        // This increases the radius when sound is louder
        float b = ampFromAudio.Amplitude;
        VFX_radius = b * multiplyWithRadius;

        // Get frequency band list for gravity
        float c = frequencyList[0] * multiplyWithFrequencyLow;
        float d = frequencyList[4] * multiplyWithFrequencyHigh;
        float e = c + d;

        VFX_gravity = new Vector3(0, -e, 0);

        visualEffect.SetFloat("VFX_Intensity", VFX_intensity);
        visualEffect.SetFloat("VFX_Radius", VFX_radius);
        visualEffect.SetVector3("VFX_Gravity", VFX_gravity);


        if (frequencyList[4] < 0.01f)
        {
            Debug.LogError("Seed 1");
            seedGradientColorKeys[0].color = fraquencyColors[0];
        }
        else if (frequencyList[4] < 0.3f)
        {
            Debug.LogError("Seed 2");
            seedGradientColorKeys[0].color = fraquencyColors[1];
        }
        else if (frequencyList[4] < 1)
        {
            Debug.LogError("Seed 3");
            seedGradientColorKeys[0].color = fraquencyColors[2];
        }
        else if (frequencyList[4] > 1)
        {
            Debug.LogError("Seed 4");
            seedGradientColorKeys[0].color = fraquencyColors[3];
        }

        seedGradient.SetKeys(seedGradientColorKeys, seedGradientAlphaKeys);
        visualEffect.SetGradient("SeedsGradient", seedGradient);
        // For color and its intesnity 

        //var intensity = (VFX_Color.r, VFX_Color.g  VFX_Color.b)
        visualEffect.SetVector4("Seed_Color", new Vector3(VFX_Color.r, VFX_Color.g, VFX_Color.b) / 0.5f);
    }
}
