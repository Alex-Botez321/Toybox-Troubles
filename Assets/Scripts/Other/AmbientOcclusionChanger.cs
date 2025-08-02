using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AmbientOcclusionChanger : MonoBehaviour
{
    [SerializeField] private PostProcessVolume postProcessAttribute;
    [SerializeField] private float speedOfChange = 0.005f;
    [SerializeField] private float lowestAmbientOcclusion = 0.7f;
    private AmbientOcclusion ambientOcclusion;
    private float magnitudeDifference;
    private float defaultAmbientOcclusion;

    private void Awake()
    {
        postProcessAttribute.profile.TryGetSettings(out ambientOcclusion);
        defaultAmbientOcclusion = ambientOcclusion.intensity;
    }

    private void FixedUpdate()
    {
        ambientOcclusion.intensity.value -= speedOfChange;
        if (ambientOcclusion.intensity < lowestAmbientOcclusion)
            ambientOcclusion.intensity.value = lowestAmbientOcclusion;
        else if(ambientOcclusion.intensity > defaultAmbientOcclusion)
            ambientOcclusion.intensity.value = defaultAmbientOcclusion;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            speedOfChange *= -1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            speedOfChange *= -1;
        }
    }
}
