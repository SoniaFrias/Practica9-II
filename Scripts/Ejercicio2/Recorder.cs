using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    private AudioSource audioSource;
    private bool grabando = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
        {
            Debug.Log("[OK] Micrófono principal detectado.");
        }
        else
        {
            Debug.Log("Unity no detecta ningún micrófono");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !grabando) 
        {
            if (Microphone.devices.Length > 0)
            {
                grabando = true;
                audioSource.Stop();
                audioSource.clip = Microphone.Start("", false, 10, 44100);
                Debug.Log("Grabando...");
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && grabando)
        {
            grabando = false;
            Microphone.End("");
            audioSource.Play();
            Debug.Log("Reproduciendo");
        }
    }

}