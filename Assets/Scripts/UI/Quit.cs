using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Start()
    {
        Application.Quit();
        Debug.Log("bye");
    }

    private void Update()
    {
        Application.Quit();
        Debug.Log("bye");
    }
    void QuitGame ()
    {
        Application.Quit ();
        Debug.Log("bye");
    }
}
