using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAudio : MonoBehaviour
{
    [SerializeField] public AudioClip[] AudioClips;
    public AudioClip currentClip;
    
    public AudioClip AudioClipGet()
    {
        int soundID = UnityEngine.Random.Range((int)0, (int)AudioClips.Length);

        return AudioClips[soundID];
    }
}
