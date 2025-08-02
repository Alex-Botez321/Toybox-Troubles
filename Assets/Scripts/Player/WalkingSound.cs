using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private GameObject WalkSource;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        WalkSource.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WalkSource.SetActive(true);
            audioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            WalkSource.SetActive(false);
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            WalkSource.SetActive(true);
            audioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            WalkSource.SetActive(false);
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            WalkSource.SetActive(true);
            audioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            WalkSource.SetActive(false);
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            WalkSource.SetActive(true);
            audioSource.Play();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            WalkSource.SetActive(false);
            audioSource.Stop();
        }
    }
}
